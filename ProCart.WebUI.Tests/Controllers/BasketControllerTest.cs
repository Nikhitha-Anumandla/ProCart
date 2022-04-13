using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProCart.core.Contracts;
using ProCart.core.Models;
using ProCart.core.ViewModels;
using ProCart.Services;
using ProCart.WebUI.Controllers;
using ProCart.WebUI.Tests.Mocks;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProCart.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //setup
            IRepository<Basket> BasketContext = new MockContext<Basket>();
            IRepository<Products> products = new MockContext<Products>();

            var httpContext = new HttpMockContext();
            IBasketService basketService = new BasketService(products, BasketContext);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);
            //Act
            /*basketService.AddToBasket(httpContext, "1");*/

            controller.AddToBasket("1");

            Basket basket = BasketContext.Collections().FirstOrDefault();
            
            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.basketItems.Count);
            Assert.AreEqual("1", basket.basketItems.ToList().FirstOrDefault().ProductId);

        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> BasketContext = new MockContext<Basket>();
            IRepository<Products> products = new MockContext<Products>();

            products.Insert(new Products() { id = "1", Price = 100 });
            products.Insert(new Products() { id = "2", Price = 100 });

            Basket basket = new Basket();
            basket.basketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.basketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            BasketContext.Insert(basket);

            IBasketService basketService = new BasketService(products,BasketContext);

            var controller = new BasketController(basketService);
            var httpContext = new HttpMockContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("ProCartBasket") { Value = basket.id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(300, basketSummary.BasketTotal);
        }

    }
}
