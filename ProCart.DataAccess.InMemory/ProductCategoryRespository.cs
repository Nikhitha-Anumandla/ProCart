using ProCart.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategories> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategories>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategories>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void InsertCategory(ProductCategories p)
        {
            productCategories.Add(p);
        }

        public void UpdateCategory(ProductCategories category)
        {
            ProductCategories ProductCategoriesToUpdate = productCategories.Find(c => c.id == category.id);
            if (ProductCategoriesToUpdate != null)
                ProductCategoriesToUpdate = category;
            else
                throw new Exception("category is not found!");
        }

        public ProductCategories FindCategory(string id)
        {
            ProductCategories productCategory = productCategories.Find(c => c.id == id);
            if (productCategory != null)
                return productCategory;
            else
                throw new Exception("product not found!");
        }

        public IQueryable<ProductCategories> GetCategories()
        {
            return productCategories.AsQueryable();
        }

        public void DeleteCategory(string id)
        {
            ProductCategories productCategory = productCategories.Find(c => c.id == id);
            if (productCategory != null)
                productCategories.Remove(productCategory);
            else
                throw new Exception("product not found!");
        }
    }
}

