using ProCart.core.Models;
using ProCart.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProCart.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategories> categories = context.GetCategories().ToList();
            return View(categories);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(ProductCategories category)
        {
            if (!ModelState.IsValid)
                return View(category);
            context.InsertCategory(category);
            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult EditCategory(string id)
        {
            var category = context.FindCategory(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(ProductCategories category, string id)
        {
            var categoryInMemory = context.FindCategory(id);
            if (categoryInMemory == null)
                return HttpNotFound();
            if (!ModelState.IsValid)
                return View(category);
            categoryInMemory.categories = category.categories;
            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(string id)
        {
            var category = context.FindCategory(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteCategory(ProductCategories category,string id)
        {
            var categoryInMemory = context.FindCategory(id);
            if (categoryInMemory == null)
                return HttpNotFound();
            context.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}