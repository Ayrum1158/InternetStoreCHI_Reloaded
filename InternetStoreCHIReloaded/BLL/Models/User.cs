using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ProductsSet UserCart { get; set; }
        public ICollection<ProductsSet> UserOrders { get; set; }
    }
}
