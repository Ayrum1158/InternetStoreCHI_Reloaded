using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class UserRegistrationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
