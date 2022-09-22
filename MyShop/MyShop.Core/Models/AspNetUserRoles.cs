using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class AspNetUserRoles : BaseEntity
    {
        public string RoleId { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUser { get; set; }
        public AspNetUserRoles()
        {
            this.AspNetUser = new List<AspNetUsers>();
        }
    }
}
