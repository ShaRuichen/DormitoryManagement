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
            this.ViewBag.Students = Sql.Read("SELECT * FROM student");
            return View();
        }

        public IActionResult ShowUnpaymentTable()
        {
            this.ViewBag.Students = Sql.Read("SELECT * FROM student WHERE charge = 'false'");
            return View();
        }

        [HttpPost]
        public IActionResult EntranceTable(string id, string open)
        {
            Sql.Execute("UPDATE student SET authority = @0 WHERE id = @1", open, id);
            return View("Index");
        }

        [HttpPost]
        public IActionResult ChangeStudent(string id, string bed)
        {
            var bedParts = bed.Split(' ');
            Sql.Execute("UPDATE student SET building = @0, room = @1 WHERE id = @2", bedParts[0], bedParts[1], id);
            return View("Index");
        }
    }
}
