using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult ManageEntrance() => View();

        public IActionResult SearchEmptyBed()
        {
            this.ViewBag.Rooms = Sql.Read("SELECT * FROM room WHERE live_in_number < 4");
            return View();
        }

        public IActionResult ChangeTable()
        {
            return View();
        }

        public IActionResult ShowStudentTable()
        {
            return View();
        }

        public IActionResult ShowUnpaymentTable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EntranceTable(string id, string open)
        {
            Sql.Execute("UPDATE student SET authority = @0 WHERE id = @1", open, id);
            return View("Index");
        }

        [HttpPost]
        public void ChangeStudent(string id, string bed)
        {

        }
    }
}
