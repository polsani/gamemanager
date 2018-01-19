using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GameManager.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameManager.Web.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
