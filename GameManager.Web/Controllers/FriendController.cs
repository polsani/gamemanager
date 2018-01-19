using GameManager.Domain.Contracts.Services;
using GameManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameManager.Web.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        public IActionResult Index()
        {
            var friends = FriendViewModel.ConvertCollectionFromDomain(_friendService.GetAll());

            return View(friends);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(string id)
        {
            var friendToEdit = FriendViewModel.ConvertFromDomain(_friendService.Get(id));

            return View(friendToEdit);
        }

        public IActionResult Details(string id)
        {
            var friendToDetail = FriendViewModel.ConvertFromDomain(_friendService.Get(id));

            return View(friendToDetail);
        }

        public IActionResult Save(FriendViewModel friend)
        {
            _friendService.Save(friend.ConvertToDomain());

            return RedirectToAction("Index");
        }
    }
}