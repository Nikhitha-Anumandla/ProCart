using ProCart.core.Contracts;
using ProCart.core.Models;
using ProCart.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProCart.Services
{
    public class BasketService:IBasketService
    {
        IRepository<Products> productContext;
        IRepository<Basket> BasketContext;
        public const string BasketSessionName = "ProCartBasket";
        public BasketService(IRepository<Products> productContext,IRepository<Basket> BasketContext)
        {
        this.productContext=productContext;
        this.BasketContext = BasketContext;
        }

        private Basket GetBasket(HttpContextBase httpContext,bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket = new Basket();
            if (cookie != null)
            {
                string BasketId = cookie.Value;
                if (!string.IsNullOrEmpty(BasketId))
                {
                    basket = BasketContext.Find(BasketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;
        }
        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            BasketContext.Insert(basket);
            BasketContext.Commit();
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.id;
            cookie.Expires = DateTime.Now.AddMinutes(1);
            httpContext.Response.Cookies.Add(cookie);
            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext,string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.basketItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.id,
                    ProductId = productId,
                    Quantity = 1
                };
                basket.basketItems.Add(item);
            }
            else
                item.Quantity++;
            BasketContext.Commit();
        }

        public void RemoveBasket(HttpContextBase httpContext,string itemId)
        {
            Basket basket = GetBasket(httpContext,true);
            BasketItem item = basket.basketItems.FirstOrDefault(m => m.id == itemId);
            if (item != null)
            {
                basket.basketItems.Remove(item);
                BasketContext.Commit();
            }

        }

        public List<BasketViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            if (basket != null)
            {
                var results = (from b in basket.basketItems
                               join p in productContext.Collections() on b.ProductId equals p.id
                               select new BasketViewModel()
                               {
                                   id = b.id,
                                   Quantity = b.Quantity,
                                   Price = p.Price,
                                   Image = p.Image,
                                   ProductName = p.Name
                               }).ToList();
                return results;
            }
            return new List<BasketViewModel>();
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel vm = new BasketSummaryViewModel(0, 0);
            if (basket != null)
            {
                int? basketCount = (from item in basket.basketItems
                                   select item.Quantity).Sum();
                decimal? basketTotal = (from item in basket.basketItems
                                        join p in productContext.Collections() on item.ProductId equals p.id
                                        select item.Quantity * p.Price).Sum();
                vm.BasketCount = basketCount ?? 0;
                vm.BasketTotal = basketTotal ?? decimal.Zero;
                return vm;
            }
            return vm;
        }

        public void ClearBasket(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext,false);
            basket.basketItems.Clear();
            BasketContext.Commit();
        }

    }
}
