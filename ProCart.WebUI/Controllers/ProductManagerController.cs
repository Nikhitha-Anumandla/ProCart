using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProCart.core.Models;
using ProCart.DataAccess.InMemory;
using ProCart.core.ViewModels;
using ProCart.core.Constracts;

namespace ProCart.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Products> context;
        IRepository<ProductCategories> categoryContext;
        public ProductManagerController(IRepository<Products> productContext, IRepository<ProductCategories> productCategoryContext)
        {
            context =productContext;
            categoryContext = productCategoryContext;
        }
        
        // GET: ProductManager
        public ActionResult Index()
        {
            var product = context.Collections().ToList();
            return View(product);
        }

        public ActionResult CreateProduct()
        {
            var vm = new ProductViewModel
            {
                product = new Products(),
                categories = categoryContext.Collections()
            };
            return View(vm);
        }
        [HttpPost]
        public ActionResult CreateProduct(Products product)
        {
            if (!ModelState.IsValid)
                return View(product);
            context.Insert(product);
            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult EditProduct(string id)
        {
            var product = context.Find(id);
            if (product == null)
                return HttpNotFound();
            var vm = new ProductViewModel
            {
                product = product,
                categories = categoryContext.Collections()
            };
            return View(vm);
        }
        [HttpPost]
        public ActionResult EditProduct(Products product,string id)
        {
            Products productToEdit = context.Find(id);
            if (productToEdit == null)
                return HttpNotFound();
            if (!ModelState.IsValid)
                return View(product);
            productToEdit.Name = product.Name;
            productToEdit.Category = product.Category;
            productToEdit.Price = product.Price;
            productToEdit.Image = product.Image;
            productToEdit.Description = product.Description;
            context.Commit();
            return RedirectToAction("Index");

        }

        public ActionResult DeleteProduct(string id)
        {
            var product = context.Find(id);
            if (product == null)
                return HttpNotFound();
            return View(product);   
        }

        [HttpPost]
        public ActionResult DeleteProduct(Products products,string Id)
        {
            var product = context.Find(Id);
            if (product == null)
                return HttpNotFound();
            context.Delete(Id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}