using DormitoryManagement.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    class TestManagerController
    {
        private ManagerController controller;

       [TestInitialize] 
        public void Initialize()
        {
            controller = new ManagerController();
        }

        //管理门禁权限_是
        [TestMethod]
        public void Test_ManageEntrance_yes()
        {
            string s_id = "12345";
            string s_open = "yes";
            controller.EntranceTable(s_id, s_open);

            var actual = DormitoryManagement.Sql.Read("SELECT id,authority FROM student WHERE id = @0,authrity = @1", s_id,s_open);
            var expect = DormitoryManagement.Sql.Read("SELECT id,authority FROM student WHERE id = @0",s_id);     

            Assert.AreEqual(actual, expect);
        }
        
        //管理门禁权限_否
        [TestMethod]
        public void Test_ManageEntrance_no()
        {
            string s_id = "12345";
            string s_open = "No";
            controller.EntranceTable(s_id, s_open);

            var actual = DormitoryManagement.Sql.Read("SELECT id,authority FROM student WHERE id = @0,authrity = @1", s_id, s_open);
            var expect = DormitoryManagement.Sql.Read("SELECT id,authority FROM student WHERE id = @0,", s_id);

            Assert.AreEqual(actual, expect);
        }


        [TestMethod]
        public void Test_ChangeStudent()
        {
            string s_id = "12345";
            string s_bed = "宿舍1 201";
            var s_bedParts = s_bed.Split(' ');

            controller.ChangeStudent(s_id, s_bed);

            var quary = DormitoryManagement.Sql.Read("SELECT * FROM student WHERE id = @0,building=@1,room=@2", s_id, s_bedParts[0], s_bedParts[1]);
            Assert.IsNotNull(quary);
        }
    }
}
