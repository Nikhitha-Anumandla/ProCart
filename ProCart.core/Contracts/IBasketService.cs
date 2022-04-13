using ProCart.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProCart.core.Contracts
{
    public interface IBasketService
    {
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
        List<BasketViewModel> GetBasketItems(HttpContextBase httpContext);
        void RemoveBasket(HttpContextBase httpContext, string itemId);
        void AddToBasket(HttpContextBase httpContext, string productId);
        void ClearBasket(HttpContextBase httpContext);
    }
}
