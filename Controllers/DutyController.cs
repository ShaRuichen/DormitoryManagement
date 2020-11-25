using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormitoryManagement.Controllers
{
    public class DutyController : Controller
    {
  
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult LateStudent()
        {
            return View();
        }

        public IActionResult sign_in_stranger(string p_id,string name,string in_time,string out_time,string type)
        {
            if (p_id != null)
            {
                Sql.Execute("INSERT INTO tableofperson VALUES (NULL,@0,@1,@2,@3,@4)",p_id, name, in_time, out_time, type);
                //Sql.Execute("INSERT INTO tableofperson(p_id,name,in-time,out-time,type) VALUES (@0,@1,@2,@3,@4)", p_id, name, in_time, out_time, type);
            }
            return View("Index");
        }

        public IActionResult sign_in_late_student(string p_id,string name,string in_time,string type)
        {
            if (p_id!= null)
            {
                Sql.Execute("INSERT INTO tableofperson VALUES(NULL,@0,@1,@2,NULL,@3)", p_id, name, in_time, type);
            }
            return View("Index");
        }
    }
}
