using ProCart.core.Constracts;
using ProCart.core.Models;
using ProCart.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProCart.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Products> context;
        IRepository<ProductCategories> categoryContext;
        public HomeController(IRepository<Products> productContext, IRepository<ProductCategories> productCategoryContext)
        {
            context = productContext;
            categoryContext = productCategoryContext;
        }
        public ActionResult Index(string category=null)
        {
            List<Products> product;
            List<ProductCategories> categories=categoryContext.Collections().ToList();
            if (category == null)
                product = context.Collections().ToList();
            else
                product = context.Collections().Where(p => p.Category == category).ToList();
            var vm = new ProductListViewModel()
            {
                products = product,
                productCatgories = categories
            };
            return View(vm);
        }

        public ActionResult Details(string id)
        {
            var product = context.Find(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}