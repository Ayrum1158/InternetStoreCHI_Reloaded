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
        public Cart Cart { get; set; }

        public User()
        {
            Cart = new Cart();
        }
    }
}
