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
        public ICollection<OrderedItem> OrderedItems { get; set; }

        public int UserId { get; set; }

        public Order()
        {
            OrderedItems = new List<OrderedItem>();
        }
    }
}
