using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;

namespace MyShop.Services
{
    public class BasketService : IBasketService
    {
        IInMemoryRepository<Product> productContext;
        IInMemoryRepository<Basket> basketContext;
        public const string BasketSessionName = "eCommerceBasket";
        public BasketService(IInMemoryRepository<Product> productContext, IInMemoryRepository<Basket> basketContext)
        {
            this.basketContext=basketContext;
            this.productContext=productContext;
        }

        private Basket GetBasket(HttpContextBase httpContext,bool createIfNull)
        {
            HttpCookie cookie;
           
            cookie = httpContext.Request.Cookies.Get(BasketSessionName);
           
            Basket basket = new Basket();
            if (cookie!=null)
            {
                string basketId = cookie.Value;
                if(!string.IsNullOrEmpty(basketId))
                {
                    basket= basketContext.Find(basketId);
                }
                else
                {
                    if(createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket= new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }
        
        public void AddToBasket(HttpContextBase httpContextBase,string productId)
        {
            Basket basket = GetBasket(httpContextBase, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
            if(item==null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1,
                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity+1;
            }
            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContextBase, string itemId)
        {
            Basket basket = GetBasket(httpContextBase, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {                
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }
        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContextBase)
        {
            Basket basket = GetBasket(httpContextBase, false);
            if (basket!=null)
            {
                var result = (from b in basket.BasketItems
                              join p in productContext.Collection() on b.ProductId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
                                  ProductName = p.Name,
                                  Image = p.Image,
                                  Price = p.Price
                              }
                              ).ToList();
                return result;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }
        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContextBase)
        {
            Basket basket = GetBasket(httpContextBase, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0,0);
            if(basket!=null)
            {
                long? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();
                decimal? basketTotalAmt = (from item in basket.BasketItems
                                           join p in productContext.Collection() on
                                           item.ProductId equals p.Id
                                           select item.Quantity * p.Price).Sum();
                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotalAmt ?? decimal.Zero;
            }            
            return model;            
        }
    }
}
