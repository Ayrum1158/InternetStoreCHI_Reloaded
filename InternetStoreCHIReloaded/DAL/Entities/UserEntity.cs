using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class UserEntity : IdentityUser<int>, IHasId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ProductsSetEntity UserCart { get; set; }
        public ICollection<ProductsSetEntity> UserOrders { get; set; }
    }
}
