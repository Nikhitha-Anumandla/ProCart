using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.core.Models
{
    public class Basket:BaseEntity
    {
        public List<BasketItem> basketItems { get; set; }
        public Basket()
        {
           basketItems = new List<BasketItem>();
        }
    }
}
