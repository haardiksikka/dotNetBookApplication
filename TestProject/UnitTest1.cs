using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.
using BookReadingEventApplication.Controllers;
namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }
    }
}
