﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myshop.WebUI;
using Myshop.WebUI.Controllers;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Myshop.WebUI.Tests.Controllers
{
    [TestClass]
   public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IInMemoryRepository<Product> productContext = new Mocks.MockContext<Product>();
            IInMemoryRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();
            productContext.Insert(new Product());
            HomeController controller = new HomeController(productContext, productCategoryContext);

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Product.Count());
        }
    }
}
