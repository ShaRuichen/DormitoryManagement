using DormitoryManagement.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test
{
    [TestClass]
    class TestDutyController
    {
        private DutyController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new DutyController();
        }

        [TestMethod]
        public void Test_sign_in_stranger_success()
        {
            var query = DormitoryManagement.Sql.Read("SELECT * FROM tableofperson");
            int preNum = query.Count;                   //调用sign_in_stranger函数前的列数

            string p_id = "12345", name = "name1", in_time = "2020/12/20-15:00", out_time = "2020/12/20-16:30", type = "stranger";

            controller.sign_in_stranger(p_id,name,in_time,out_time,type);
           
            int postNum = query.Count;                  //调用sign_in_stranger函数后的列数
            var result = (preNum == postNum-1);         //preNum与postNum的比较，如果两个值一致返回true

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Test_sign_in_stranger_failed()
        {
            var query = DormitoryManagement.Sql.Read("SELECT * FROM tableofperson");
            int preNum = query.Count;                   //调用sign_in_stranger函数前的列数

            string p_id = null, name = "name1", in_time = "2020/12/20-15:00", out_time = "2020/12/20-16:30", type = "stranger";

            controller.sign_in_stranger(p_id, name, in_time, out_time, type);

            int postNum = query.Count;                  //调用sign_in_stranger函数后的列数
            var result = (preNum != postNum - 1);       //preNum与postNum的比较，如果两个值不一致返回true

            Assert.IsTrue(result);
        }
        
       //虽然数据库里p_id定义int型，但是在这里它用string型。因为修改int型了就不能用sign_in_stranger里的if(p_id!=null)这条件
         
         



        [TestMethod]
        public void Test_sign_in_late_student_success()
        {
            var query = DormitoryManagement.Sql.Read("SELECT * FROM tableofperson");
            int preNum = query.Count;                   //调用sign_in_late_student函数前的列数

            string p_id = "student1", name = "name1", in_time = "2020/12/21-23:30", type = "late student";
            controller.sign_in_late_student(p_id, name, in_time, type);

            int postNum = query.Count;                  //调用sign_in_late_student函数后的列数
            var result = (preNum == postNum - 1);       //preNum与postNum的比较，如果两个值一致返回true

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_sign_in_late_student_failed()
        {
            var query = DormitoryManagement.Sql.Read("SELECT * FROM tableofperson");
            int preNum = query.Count;                   //调用sign_in_late_student函数前的列数

            string p_id = null, name = "name1", in_time = "2020/12/21-23:30", type = "late student";
            controller.sign_in_late_student(p_id, name, in_time, type);

            int postNum = query.Count;                  //调用sign_in_late_student函数后的列数
            var result = (preNum != postNum - 1);       //preNum与postNum的比较，如果两个值不一致返回true

            Assert.IsTrue(result);
        }
    }
}


