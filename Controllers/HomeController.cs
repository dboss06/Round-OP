using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Round_OP.Models;
using System.Diagnostics;

namespace Round_OP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
