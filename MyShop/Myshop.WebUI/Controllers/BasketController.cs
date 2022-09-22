using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        // GET: Basket
        IBasketService basketService;
        IOrderService orderService;
        IInMemoryRepository<Customer> customerRepository;
        public BasketController(IBasketService basketService, IOrderService orderService, IInMemoryRepository<Customer> customers)
        {
            this.basketService = basketService;
            this.orderService = orderService;
            this.customerRepository = customers;
        }

        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model); 
        }
        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }
        [Authorize]
        public ActionResult CheckOut()
        {
            Customer customer = customerRepository.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if(customer!=null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    SurName = customer.LastName,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    ZipCode = customer.ZipCode

                };
                return View(order);
            }
            else
            {
                RedirectToAction("Error");
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            //Payment Process

            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("ThankYou", new { OrderId = order.Id });


        }

        public ActionResult ThankYou (String orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}