using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.core.ViewModels
{
    public class BasketViewModel
    {
        public string id { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }


    }
}
