using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class NewUserDbModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
