using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product : BaseEntity
    {
        //public string Id { get; set; }
        [StringLength(30)]
        [DisplayName("Product")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0,10000)]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Catagery { get; set; }
        public string Image { get; set; }

        //public Product()
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //}
    }
}
