using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class CartItemEntity : IHasId
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductEntity Product { get; set; }
        public int ProductId { get; set; }

        public IEnumerable<CartEntity> Carts { get; set; }// nav prop
    }
}
