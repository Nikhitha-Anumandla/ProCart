using ProCart.core.Contracts;
using ProCart.core.Models;
using ProCart.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProCart.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketSerive;
        IOrderService orderService;
        IRepository<Customer> customerService;

        public BasketController(IBasketService BasketSerive, IOrderService orderService, IRepository<Customer> customerService)
        {
            this.basketSerive = BasketSerive;
            this.orderService = orderService;
            this.customerService = customerService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            
            var model = basketSerive.GetBasketItems(this.HttpContext);
            return View(model);
        }
        public ActionResult AddToBasket(string id)
        {
            basketSerive.AddToBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string id)
        {
            basketSerive.RemoveBasket(this.HttpContext, id);
            return RedirectToAction("Index");

        }

        public PartialViewResult BasketSummary()
        {
            var basketSumary = basketSerive.GetBasketSummary(this.HttpContext);
            return PartialView(basketSumary);
        }
        
        [Authorize]
        public ActionResult CheckOut()
        {
            Customer customer = customerService.Collections().FirstOrDefault(m => m.Email == User.Identity.Name);
            if (customer != null)
            {
                Order order = new Order()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    City = customer.City,
                    Email = customer.Email,
                    State = customer.State,
                    Street = customer.Street,
                    ZipCode = customer.ZipCode
                };
                return View(order);
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(Order order)
        {
            var basketItems = basketSerive.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            //payment process

            order.OrderStatus = "payment processed";
            orderService.CreateOrder(order, basketItems);
            basketSerive.ClearBasket(this.HttpContext);
            return RedirectToAction("ThankYou", new { OrderId = order.id });

        }

        public ActionResult ThankYou(string orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}