using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class OrderEntity : IHasId
    {
        public int Id { get; set; }

        // OrderHeader:
        public DateTime CreationDate { get; set; }
        public decimal TotalSum { get; set; }

        // Order items:
        public ICollection<OrderedItemEntity> OrderedItems { get; set; }

        public int UserId { get; set; }

        public OrderEntity()
        {
            OrderedItems = new List<OrderedItemEntity>();
        }
    }
}
