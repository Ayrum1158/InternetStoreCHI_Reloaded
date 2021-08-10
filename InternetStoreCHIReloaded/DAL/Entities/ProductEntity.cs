using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ProductEntity : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public CategoryEntity Category { get; set; }
        public int CategoryId { get; set; }
    }
}
