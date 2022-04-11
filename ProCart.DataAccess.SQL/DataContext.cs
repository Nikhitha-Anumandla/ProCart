using ProCart.core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.DataAccess.SQL
{
    public class DataContext:DbContext
    {
        public DataContext()
            :base("DefaultConnection")
        {

        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }

    }
}
