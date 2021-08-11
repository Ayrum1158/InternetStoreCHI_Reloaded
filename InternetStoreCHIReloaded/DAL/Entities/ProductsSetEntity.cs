using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ProductsSetEntity : IHasId
    {
        public int Id { get; set; }
        public ICollection<ProductWithQuantityEntity> Products { get; set; }

        public IEnumerable<UserEntity> Users { get; set; }// nav prop

        public ProductsSetEntity()
        {
            Products = new List<ProductWithQuantityEntity>();
        }
    }
}
