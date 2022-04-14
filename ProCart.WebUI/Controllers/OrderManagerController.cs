using ProCart.core.Contracts;
using ProCart.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProCart.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {

        IOrderService orderContext;
        public OrderManagerController(IOrderService orderContext)
        {
            this.orderContext = orderContext;
        }
        // GET: OrderManager
        public ActionResult Index()
        {
            var orders = orderContext.GetOrders();
            return View(orders);
        }

        public ActionResult UpdateOrder(string id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order shipped",
                "Order delivered"
            };
            var order = orderContext.GetOrder(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Order orderToUpdate,string id)
        {
            var order = orderContext.GetOrder(id);
            order.OrderStatus = orderToUpdate.OrderStatus;
            orderContext.UpdateOrder(order);

            return RedirectToAction("Index");
        }
    }
}