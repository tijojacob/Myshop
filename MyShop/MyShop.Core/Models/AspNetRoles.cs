using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class AspNetRoles : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUser { get; set; }
        public AspNetRoles()
        {
            this.AspNetUser = new List<AspNetUsers>();
        }
    }
}
