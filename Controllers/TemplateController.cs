using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Students = Sql.Read("SELECT id FROM student");

            return View();
        }

        public IActionResult Modify(string id)
        {
            if (id != null)
            {
                Sql.Execute("INSERT INTO student VALUES(@0, 'name', 'sex', 'college', " +
                    "'stage', 'class', 'building', 'room', 'authority', 'charge', 'charge_urge')", id);
            }
            else
            {
                Sql.Execute("DELETE FROM student");
            }

            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public JsonResult Click(string name, int number)
        {
            return Json(new
            {
                name,
                num = number + 1,
            });
        }
    }
}
