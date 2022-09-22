using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class AspNetUserLogins : BaseEntity
    {
        public virtual ICollection<AspNetUsers> AspNetUser { get; set; }
        public AspNetUserLogins()
        {
            this.AspNetUser = new List<AspNetUsers>();
        }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
