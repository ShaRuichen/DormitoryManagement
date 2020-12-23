using DormitoryManagement.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestHomeController
    {
        private HomeController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new HomeController();
        }

        [TestMethod]
        public void Test_sing_up_manager()
        {
            var actual = (RedirectToActionResult)controller.sing_up("123", "123");
            var expect = controller.RedirectToAction("Index", "Manager");
            Assert.AreEqual(actual.ControllerName, expect.ControllerName);
            Assert.AreEqual(actual.ActionName, expect.ActionName);
        }

        [TestMethod]
        public void Test_sing_up_duty()
        {
            var actual = (RedirectToActionResult)controller.sing_up("456", "456");
            var expect = controller.RedirectToAction("Index", "Duty");
            Assert.AreEqual(actual.ControllerName, expect.ControllerName);
            Assert.AreEqual(actual.ActionName, expect.ActionName);
        }

        [TestMethod]
        public void Test_sing_up_failed()
        {
            var actual = (ViewResult)controller.sing_up("123", "456");
            var expect = controller.View("Login");
            Assert.AreEqual(actual.ViewName, expect.ViewName);
        }
    }
}
