using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace Myshop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context = new ProductRepository();
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            Product p = new Product();
            return View(p);
        }
        [HttpPost]
        public ActionResult Create(Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(newProduct);
            }
            else
            {
                context.Insert(newProduct);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else 
            { 
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product updatedProduct, string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(updatedProduct);
                }
                else
                {
                    context.Update(updatedProduct);
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        [ActionName("ConfirmDelete")]
        public ActionResult DeleteProduct(String Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(productToDelete);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        
        public ActionResult ConfirmDelete(String Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }            
        }       
    }
}