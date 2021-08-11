using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class ProductsSet : IHasId
    {
        public int Id { get; set; }
        public ICollection<ProductWithQuantity> Products { get; set; }

        public ProductsSet()
        {
            Products = new List<ProductWithQuantity>();
        }
    }
}
