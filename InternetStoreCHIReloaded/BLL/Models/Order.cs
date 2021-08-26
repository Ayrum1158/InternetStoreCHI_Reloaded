using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class Order
    {
        public int Id { get; set; }

        // OrderHeader:
        public DateTime CreationDate { get; set; }
        public decimal TotalSum { get; set; }

        // Order items:
        public ICollection<OrderedProduct> OrderedProducts { get; set; }

        public int UserId { get; set; }

        public Order()
        {
            OrderedProducts = new List<OrderedProduct>();
        }
    }
}
