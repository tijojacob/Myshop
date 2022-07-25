using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.ViewModel;

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
        public ActionResult Index(String Category = null)
        {
            //List<Product> products = context.Collection().ToList();
            List<Product> products;
            List<ProductCategory> categories = productCategoryRepository.Collection().ToList();
            if(Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p=>p.Catagery== Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Product=products;
            model.ProductCategories=categories;
            return View(model);
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