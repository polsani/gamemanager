using GameManager.Domain.Contracts.Services;
using GameManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameManager.Web.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var games = GameViewModel.ConvertCollectionFromDomain(_gameService.GetAll());

            return View(games);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(string id)
        {
            var gameToEdit = GameViewModel.ConvertFromDomain(_gameService.Get(id));

            return View(gameToEdit);
        }

        public IActionResult Details(string id)
        {
            var gameToDetail = GameViewModel.ConvertFromDomain(_gameService.Get(id));

            return View(gameToDetail);
        }

        [HttpPost]
        public IActionResult Save(GameViewModel game)
        {
            _gameService.Save(game.ConvertToDomain());

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            _gameService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string gameTitle)
        {
            var games = _gameService.GetByTitle(gameTitle);

            return Json(GameViewModel.ConvertCollectionFromDomain(games));
        }
    }
}