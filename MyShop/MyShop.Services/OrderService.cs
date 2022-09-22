using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;

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
        public List<Order> GetOrderList()
        {
            return orderContent.Collection().ToList();
        }
        public Order GetOrder(string Id)
        {
            return orderContent.Find(Id);
        }
        public void UpdateOrder(Order updateOrder)
        {
            orderContent.Update(updateOrder);
            orderContent.Commit(); 
        }
    }
}
