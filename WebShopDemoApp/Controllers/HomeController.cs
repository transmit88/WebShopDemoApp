using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShopDemoApp.Models;

namespace WebShopDemoApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //Cookie
            //if (TempData.ContainsKey("LastAccessTime"))
            //{
            //    return Ok(TempData["LastAccessTime"]);
            //}

            //TempData["LastAccessTime"] = DateTime.Now;

            //this.HttpContext.Response.Cookies.Append("mCookie", "Pesho");

            //Session
            this.HttpContext.Session.SetString("name", "pesho");

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            //Session
            //string? name = this.HttpContext.Session.GetString("name");

            //if (!string.IsNullOrEmpty(name))
            //{
            //    return Ok(name);
            //}

            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}