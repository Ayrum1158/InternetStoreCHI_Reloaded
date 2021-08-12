using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }

        // Cart items:
        public ICollection<ProductWithQuantity> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<ProductWithQuantity>();
        }
    }
}
