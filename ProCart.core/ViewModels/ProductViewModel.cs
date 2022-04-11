using ProCart.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProCart.core.ViewModels
{
    public class ProductViewModel
    {
        public Products product { get; set; }
        public IEnumerable<ProductCategories> categories { get; set; }
    }
}