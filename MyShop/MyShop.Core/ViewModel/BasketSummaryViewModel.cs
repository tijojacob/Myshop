using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModel
{
    public class BasketSummaryViewModel
    {
        public long BasketCount { get; set; }
        public decimal BasketTotal { get; set; }
        public BasketSummaryViewModel()
        {

        }
        public BasketSummaryViewModel(long basketCount,decimal basketTotal)
        {
            this.BasketTotal = basketTotal;
            this.BasketCount = basketCount;
        }
    }
}
