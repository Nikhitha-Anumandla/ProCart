using ProCart.core.Constracts;
using ProCart.core.Models;
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
        public ActionResult Index()
        {
            var products = context.Collections().ToList();
            return View(products);
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