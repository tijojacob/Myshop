using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.DataAccess.InMemory;

namespace Myshop.WebUI.Controllers
{
    [CustomAuthorize]
    public class ProductManagerController : Controller
    {
        //ProductRepository context;
        //ProductCategoryRepository productCategoryRepository;

        IInMemoryRepository<Product> context;
        IInMemoryRepository<ProductCategory> productCategoryRepository;

        // GET: ProductManager
        public ProductManagerController(IInMemoryRepository<Product> conText, IInMemoryRepository<ProductCategory> proDuctCategoryRepository)
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
        public ActionResult Create(Product product,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file!=null)
                {
                    product.Image= product.Id + Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                context.Insert(product);
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
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    ProductManagerViewModel viewModel = new ProductManagerViewModel();
                    viewModel.Product = product;
                    viewModel.ProductCategories = productCategoryRepository.Collection();
                    return View(viewModel);
                }
                else
                {
                    if (file != null)
                    {
                        var path = Server.MapPath("//Content//ProductImages//") + productToEdit.Image;
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        productToEdit.Image = product.Id + Path.GetFileName(file.FileName);
                        file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                    }
                    //context.Update(productToEdit);
                    productToEdit.Catagery = product.Catagery;
                    productToEdit.Description = product.Description;
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;

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
                var path=Server.MapPath("//Content//ProductImages//") + productToDelete.Image;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
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