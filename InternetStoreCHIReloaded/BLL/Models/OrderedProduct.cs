using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class OrderedItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
