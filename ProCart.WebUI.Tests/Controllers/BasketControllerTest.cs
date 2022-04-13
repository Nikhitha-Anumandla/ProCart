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
            IRepository<Order> orders = new MockContext<Order>(); 

            var httpContext = new HttpMockContext();
            IBasketService basketService = new BasketService(products, BasketContext);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService,orderService);
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
            IRepository<Order> orders = new MockContext<Order>();

            products.Insert(new Products() { id = "1", Price = 100 });
            products.Insert(new Products() { id = "2", Price = 100 });

            Basket basket = new Basket();
            basket.basketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.basketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            BasketContext.Insert(basket);

            IBasketService basketService = new BasketService(products,BasketContext);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService,orderService);
            var httpContext = new HttpMockContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("ProCartBasket") { Value = basket.id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(300, basketSummary.BasketTotal);
        }

        [TestMethod]
        public void CanCheckOutAndCreateOrder()
        {
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Products> products = new MockContext<Products>();
            products.Insert(new Products(){id = "1",Price = 100});
            products.Insert(new Products(){id = "2",Price = 200});

            IRepository<Basket> baskets = new MockContext<Basket>();
            Basket basket = new Basket();
            basket.basketItems.Add(new BasketItem { ProductId = "1", Quantity = 2, BasketId = basket.id });
            basket.basketItems.Add(new BasketItem { ProductId = "2", Quantity = 1, BasketId = basket.id });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService, orderService);
            var httpContext = new HttpMockContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("ProCartBasket") { 
                Value=basket.id
            });
            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //act
            Order order = new Order();
            controller.CheckOut(order);

            //assert
            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.basketItems.Count);

            Order orderInRep = orders.Find(order.id);
            Assert.AreEqual(2, orderInRep.OrderItems.Count);
        }

    }
}
