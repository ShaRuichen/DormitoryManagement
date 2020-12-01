using DormitoryManagement.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DormitoryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult sing_up(string user, string password)
        {
            var result = Sql.Read("select * from staff where id=@0 and password=@1", user, password);
            if (result.Count == 0)
            {
                this.ViewData["failed"] = true;
                return View("Login");
            }
            if (result[0]["position"].ToString() == "manager")
            {
                return RedirectToAction("Index", "Manager");
            }
            else
            {
                return RedirectToAction("Index", "Duty");
            }
        }
    }
}
