using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class AspNetUserClaims : BaseEntity
    {
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUser { get; set; }
        public AspNetUserClaims()
        {
            this.AspNetUser = new List<AspNetUsers>();
        }
    }
}
