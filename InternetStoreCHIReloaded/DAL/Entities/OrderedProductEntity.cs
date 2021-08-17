using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class OrderedProductEntity : IHasId
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductEntity Product { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderedDate { get; set; }
        public decimal OrderedPrice { get; set; }

        public IEnumerable<OrderEntity> Orders { get; set; }// nav prop
    }
}
