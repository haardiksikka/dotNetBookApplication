using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookReadingEventApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookReadingEventApplication.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void CreateUserView()
        {
            var controller = new HomeController();
            var result = controller.CreateUser() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void LoginView()
        {
            var controller = new HomeController();
            var result = controller.Login() as ViewResult;
            Assert.AreEqual("Login", result.ViewName);
        }
        [TestMethod]
        public void UserViewView()
        {
            var controller = new HomeController();
            var result = controller.UserView() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DetailsView()
        {
            var controller = new HomeController();
            var result = controller.Details(1) as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}