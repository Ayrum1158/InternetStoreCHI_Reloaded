using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAL.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public IEnumerable<ProductEntity> Products { get; set; }
    }
}
