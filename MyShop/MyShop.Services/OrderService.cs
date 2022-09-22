using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using System.Collections.Generic;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        IInMemoryRepository<Order> orderContent;
        public OrderService(IInMemoryRepository<Order> OrderContent)
        {
            this.orderContent=OrderContent;
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
             foreach (var item in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity

                });
            }
            orderContent.Insert(baseOrder);
            orderContent.Commit();
        }
    }
}
