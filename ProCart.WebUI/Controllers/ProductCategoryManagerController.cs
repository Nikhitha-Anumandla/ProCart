using ProCart.core.Constracts;
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
        IRepository<ProductCategories> context;

        public ProductCategoryManagerController(IRepository<ProductCategories> context)
        {
            this.context = context;
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategories> categories = context.Collections().ToList();
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
            context.Insert(category);
            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult EditCategory(string id)
        {
            var category = context.Find(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(ProductCategories category, string id)
        {
            var categoryInMemory = context.Find(id);
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
            var category = context.Find(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteCategory(ProductCategories category,string id)
        {
            var categoryInMemory = context.Find(id);
            if (categoryInMemory == null)
                return HttpNotFound();
            context.Delete(id);
            return RedirectToAction("Index");
        }
    }
}