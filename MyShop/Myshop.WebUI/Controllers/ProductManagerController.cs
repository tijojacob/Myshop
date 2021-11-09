using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.DataAccess.InMemory;

namespace Myshop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productCategoryRepository;
        // GET: ProductManager
        public ProductManagerController()
        {
            context = new ProductRepository();
            productCategoryRepository = new ProductCategoryRepository();
        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product= new Product();
            viewModel.ProductCategories = productCategoryRepository.Collection();
            return View(viewModel);
            //Product p = new Product();
            //return View(p);
        }
        [HttpPost]
        public ActionResult Create(ProductManagerViewModel newProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(newProduct.Product);
            }
            else
            {
                context.Insert(newProduct.Product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Create(Product newProduct)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(newProduct);
        //    }
        //    else
        //    {
        //        context.Insert(newProduct);
        //        context.Commit();
        //        return RedirectToAction("Index");
        //    }
        //}

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else 
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategoryRepository.Collection();
                return View(viewModel);
                //return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductManagerViewModel updatedProduct, string Id)
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
                    return View(updatedProduct.Product);
                }
                else
                {
                    context.Update(updatedProduct.Product);
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }
        //public ActionResult Edit(Product updatedProduct, string Id)
        //{
        //    Product product = context.Find(Id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(updatedProduct);
        //        }
        //        else
        //        {
        //            context.Update(updatedProduct);
        //            context.Commit();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //}
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