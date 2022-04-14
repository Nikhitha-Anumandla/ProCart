using ProCart.core.Models;
using ProCart.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<BasketViewModel> BasketItems);
        List<Order> GetOrders();
        Order GetOrder(string id);
        void UpdateOrder(Order orderToUpdate);
    }
}
