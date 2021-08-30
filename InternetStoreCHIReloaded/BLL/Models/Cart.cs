using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class Cart
    {
        public int UserId { get; set; }

        // Cart items:
        public ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
