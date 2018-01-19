using GameManager.Domain.Contracts.Services;
using GameManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameManager.Web.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IGameService _gameService;
        private readonly IFriendService _friendService;
        private readonly IDunService _dunService;

        public LoanController(ILoanService loanService, IGameService gameService, IFriendService friendService, IDunService dunService)
        {
            _loanService = loanService;
            _gameService = gameService;
            _friendService = friendService;
            _dunService = dunService;
        }

        public IActionResult Index()
        {
            var loans = _loanService.GetActiveLoans();
            return View(LoanViewModel.ConvertCollectionFromDomain(loans));
        }

        public IActionResult Create()
        {
            var newLoan = new LoanViewModel
            {
                Friends = FriendViewModel.ConvertCollectionFromDomain(_friendService.GetAll()),
                Games = GameViewModel.ConvertCollectionFromDomain(_gameService.GetAvailableGames()),
                LendDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(1)
            };

            return View(newLoan);
        }

        public IActionResult Save(LoanViewModel loanViewModel)
        {
            _loanService.Lend(loanViewModel.ConvertToDomain());

            return RedirectToAction("Index");
        }

        public IActionResult Return(string id)
        {
            var loan = _loanService.Get(id);
            loan.ReturnedDate = DateTime.Now;

            return View(LoanViewModel.ConvertFromDomain(loan));
        }

        public IActionResult Details(string id)
        {
            var loan = _loanService.Get(id);

            return View(LoanViewModel.ConvertFromDomain(loan));
        }

        public IActionResult SaveReturn(LoanViewModel loanViewModel)
        {
            _loanService.Return(loanViewModel.ConvertToDomain());

            return RedirectToAction("Index");
        }

        public IActionResult AskForReturn()
        {
            _dunService.AskLateLoansForReturn();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            _loanService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}