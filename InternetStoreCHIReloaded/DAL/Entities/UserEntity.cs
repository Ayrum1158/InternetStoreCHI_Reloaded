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
        public virtual CartEntity UserCart { get; set; }
        public ICollection<OrderEntity> UserOrders { get; set; }

        public UserEntity()
        {
            UserCart = new CartEntity();
            UserOrders = new List<OrderEntity>();
        }
    }
}
