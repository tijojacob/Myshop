using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myshop.WebUI.Controllers;
using Myshop.WebUI.Tests.Mocks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Myshop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //setup
            IInMemoryRepository<Basket> baskets = new MockContext<Basket>();
            IInMemoryRepository<Product> products= new MockContext<Product>();

            IBasketService basketService = new BasketService(products, baskets);
            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();
            controller.ControllerContext= new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);
            //basketService.AddToBasket(httpContext,"1");
            
            //Act
            controller.AddToBasket("1");
            Basket basket = baskets.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count());
            Assert.AreEqual("1", basket.BasketItems.FirstOrDefault().ProductId);
            //basketService.RemoveFromBasket(httpContext, "2");
        }
        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            //setup
            IInMemoryRepository<Basket> baskets = new MockContext<Basket>();
            IInMemoryRepository<Product> products = new MockContext<Product>();

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() {ProductId ="1", Quantity=2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 3 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(5, basketSummary.BasketCount);
            Assert.AreEqual(35.00m, basketSummary.BasketTotal); 
        }
    }
}
