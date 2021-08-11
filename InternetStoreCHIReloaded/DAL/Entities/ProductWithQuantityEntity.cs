using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ProductWithQuantityEntity : IHasId
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductEntity Product { get; set; }
        public int ProductId { get; set; }

        public IEnumerable<ProductsSetEntity> ProductsSets { get; set; }// nav prop
    }
}
