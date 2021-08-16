using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class CartEntity : IHasId
    {
        public int Id { get; set; }

        // CartHeader:
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public DateTime CreationDate { get; set; }

        // Cart items:
        public ICollection<ProductWithQuantityEntity> CartItems { get; set; }

        public CartEntity()// ctor
        {
            CartItems = new List<ProductWithQuantityEntity>();
            CreationDate = DateTime.UtcNow;
        }
    }
}
