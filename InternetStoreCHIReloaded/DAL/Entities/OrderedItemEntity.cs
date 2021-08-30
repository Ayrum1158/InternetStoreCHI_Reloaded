using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class OrderedItemEntity : IHasId
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductEntity Product { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
    }
}
