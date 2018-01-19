using System;
using System.Collections.Generic;
using System.Linq;
using GameManager.Domain.Contracts.Data;
using GameManager.Domain.Contracts.Services;
using GameManager.Domain.Entities;

namespace GameManager.Application.Services
{
    public class LoanService : ILoanService
    {
        private IUnitOfWork _unitOfWork;

        public LoanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(string id)
        {
            var loan = Get(id);

            var game = _unitOfWork.GameRepository.Get(loan.GameId);
            game.Borrowed = false;

            _unitOfWork.LoanRepository.Delete(loan);

            _unitOfWork.Save();
        }

        public Loan Get(string id)
        {
            return _unitOfWork.LoanRepository.Get(id, x => x.BorrowedGame, x => x.Friend);
        }

        public IEnumerable<Loan> GetActiveLoans()
        {
            return _unitOfWork.LoanRepository.Get(x => x.ReturnedDate == null, x => x.BorrowedGame, x => x.Friend).OrderBy(x=>x.ReturnDate);
        }

        public IEnumerable<Loan> GetAll()
        {
            return _unitOfWork.LoanRepository.GetAll();
        }

        public IEnumerable<Loan> GetLateLoans()
        {
            return _unitOfWork.LoanRepository.Get(x => !x.ReturnedDate.HasValue && x.ReturnDate.Date < DateTime.Now.Date && !x.AlreadyAskedForReturn, 
                x => x.BorrowedGame, x => x.Friend);
        }

        public void Lend(Loan loan)
        {
            var game = _unitOfWork.GameRepository.Get(loan.GameId);
            game.Borrowed = true;

            loan.Id = Guid.NewGuid().ToString();

            _unitOfWork.LoanRepository.Add(loan);

            _unitOfWork.Save();
        }

        public void Return(Loan loan)
        {
            var loanToReturn = Get(loan.Id);

            loanToReturn.ReturnedDate = loan.ReturnedDate;
            loanToReturn.BorrowedGame.Borrowed = false;

            _unitOfWork.Save();
        }

        public void Update(Loan loan)
        {
            _unitOfWork.LoanRepository.Update(loan);
            _unitOfWork.Save();
        }
    }
}
