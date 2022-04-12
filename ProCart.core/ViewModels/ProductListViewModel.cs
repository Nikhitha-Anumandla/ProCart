using ProCart.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.core.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Products> products { get; set; }
        public IEnumerable<ProductCategories> productCatgories { get; set; }
    }
}
