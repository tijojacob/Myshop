using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;

namespace Myshop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IInMemoryRepository<Product> context;
        IInMemoryRepository<ProductCategory> productCategoryRepository;

        // GET: ProductManager
        public HomeController(IInMemoryRepository<Product> conText, IInMemoryRepository<ProductCategory> proDuctCategoryRepository)
        {
            //context = new ProductRepository();
            //productCategoryRepository = new ProductCategoryRepository();

            //context = new InMemoryRepository<Product>();
            //productCategoryRepository = new InMemoryRepository<ProductCategory>();

            context = conText;
            productCategoryRepository = proDuctCategoryRepository;

        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Details( string Id )
        {
            Product prd = context.Find(Id);
            if (prd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(prd);
            }
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