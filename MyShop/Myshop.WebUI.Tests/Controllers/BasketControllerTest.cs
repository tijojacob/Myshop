using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myshop.WebUI.Controllers;
using Myshop.WebUI.Tests.Mocks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.Services;
using System;
using System.Linq;
using System.Security.Principal;
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
            IInMemoryRepository<Product> products = new MockContext<Product>();
            IInMemoryRepository<Order> orders = new MockContext<Order>();
            IInMemoryRepository<Customer> customer = new MockContext<Customer>();

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService, orderService, customer);
            var httpContext = new MockHttpContext();
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);
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
            IInMemoryRepository<Order> orders = new MockContext<Order>();
            IInMemoryRepository<Customer> customer = new MockContext<Customer>();

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 3 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService, orderService, customer);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(5, basketSummary.BasketCount);
            Assert.AreEqual(35.00m, basketSummary.BasketTotal);
        }

        [TestMethod]
        public void CanCheckoutAndCreateOrder()
        {
            //setup
            IInMemoryRepository<Product> products = new MockContext<Product>();
            IInMemoryRepository<Basket> baskets = new MockContext<Basket>();
            IInMemoryRepository<Order> orders = new MockContext<Order>();
            IInMemoryRepository<Customer> customer = new MockContext<Customer>();

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2, BasketId = basket.Id });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 3, BasketId = basket.Id });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);

            var controller = new BasketController(basketService, orderService,customer);
            var httpContext = new MockHttpContext();
            customer.Insert(new Customer() { Id = "1", Email = "test@gmail.com", ZipCode = "test" });
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("test@gmail.com", "Forms"), null);
            httpContext.User = fakeUser;
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);
            
            //Act
            Order order = new Order();
            controller.CheckOut(order);

            //Assert
            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);

            Order orderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, orderInRep.OrderItems.Count);
        }
    }
}
