using DormitoryManagement.Models;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace DormitoryManagement.Controllers
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
