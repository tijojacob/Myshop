using MyShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContextBase, string productId);
        void RemoveFromBasket(HttpContextBase httpContextBase, string itemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContextBase);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContextBase);
        void ClearBasket(HttpContextBase httpContext);
    }
}
