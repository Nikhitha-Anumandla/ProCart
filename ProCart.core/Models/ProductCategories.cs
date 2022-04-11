using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.core.Models
{
    public class ProductCategories
    {
        public string id { get; set; }
        public string categories { get; set; }
        public ProductCategories()
        {
            this.id = Guid.NewGuid().ToString();
        }
    }
}
