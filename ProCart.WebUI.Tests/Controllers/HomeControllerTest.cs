using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProCart.core.Constracts;
using ProCart.core.Models;
using ProCart.core.ViewModels;
using ProCart.WebUI;
using ProCart.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ProCart.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Products> productsContext = new Mocks.MockContext<Products>();
            IRepository<ProductCategories> productCategoriesContext = new Mocks.MockContext<ProductCategories>();

            productsContext.Insert(new Products());
            HomeController controller = new HomeController(productsContext, productCategoriesContext);
            var result = controller.Index() as ViewResult;
            var ViewModel = (ProductListViewModel)result.ViewData.Model;
            Assert.AreEqual(1, ViewModel.products.Count());
        }


    }
}
