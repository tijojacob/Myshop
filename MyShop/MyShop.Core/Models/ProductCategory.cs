using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        public string Id { get; set; }
        [Required]
        public string Catagery { get; set; }
        public ProductCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
