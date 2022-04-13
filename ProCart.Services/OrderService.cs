using ProCart.core.Contracts;
using ProCart.core.Models;
using ProCart.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.Services
{
    public class OrderService:IOrderService
    {
        IRepository<Order> OrderContext;
        public OrderService(IRepository<Order> orderContext)
        {
            OrderContext = orderContext;
        }

        public void CreateOrder(Order baseOrder, List<BasketViewModel> BasketItems)
        {
            foreach(var item in BasketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.id,
                    Price = item.Price,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    ProductName=item.ProductName
                    
                });
            }
            OrderContext.Insert(baseOrder);
            OrderContext.Commit();
        }
    }
}
