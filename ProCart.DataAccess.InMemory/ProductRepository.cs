using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using ProCart.core.Models;

namespace ProCart.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Products> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Products>;
            if (products == null)
            {
                products = new List<Products>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Products p)
        {
            products.Add(p);
        }

        public void Update(Products p)
        {
            Products ProductToUpdate = products.Find(c => c.id == p.id);
            if (ProductToUpdate != null)
                ProductToUpdate = p;
            else
                throw new Exception("product not found!");
        }

        public Products Find(string id)
        {
            Products product = products.Find(c => c.id == id);
            if (product != null)
                return product;
            else
                throw new Exception("product not found!");
        }

        public IQueryable<Products> Collections()
        {
            return products.AsQueryable();
        }

        public void Delete(string id)
        {
            Products product = products.Find(c => c.id == id);
            if (product != null)
                products.Remove(product);
            else
                throw new Exception("product not found!");
        }
    }
}
