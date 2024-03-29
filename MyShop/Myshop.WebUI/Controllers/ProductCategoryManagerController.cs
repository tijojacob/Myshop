﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace Myshop.WebUI.Controllers
{
    [CustomAuthorize]
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategory
        //ProductCategoryRepository context;
        //InMemoryRepository<ProductCategory> context;
        IInMemoryRepository<ProductCategory> context;
        public ProductCategoryManagerController(IInMemoryRepository<ProductCategory> conText)
        {
            //context = new ProductCategoryRepository();
            context = conText;
        }
        public ActionResult Index()
        {
            List<ProductCategory> productsCateg = context.Collection().ToList();
            return View(productsCateg);
        }
        public ActionResult Create()
        {
            ProductCategory p = new ProductCategory();
            return View(p);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory newProductCateg)
        {
            if (!ModelState.IsValid)
            {
                return View(newProductCateg);
            }
            else
            {
                context.Insert(newProductCateg);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCateg = context.Find(Id);
            if (productCateg == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCateg);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory updatedProductCateg, string Id)
        {
            ProductCategory productCateg = context.Find(Id);
            if (productCateg == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(updatedProductCateg);
                }
                else
                {
                    productCateg.Catagery = updatedProductCateg.Catagery;
                    //context.Update(updatedProductCateg);
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        [ActionName("ConfirmDelete")]
        public ActionResult DeleteProductCat(String Id)
        {
            ProductCategory productCatToDelete = context.Find(Id);
            if (productCatToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(productCatToDelete);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult ConfirmDelete(String Id)
        {
            ProductCategory product = context.Find(Id);
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