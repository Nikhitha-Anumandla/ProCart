using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.core.Models
{
    public class Basket:BaseEntity
    {
        public virtual ICollection<BasketItem> basketItems { get; set; }
        public Basket()
        {
           this.basketItems = new List<BasketItem>();
        }
    }
}
