using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Contracts
{
    public class CategoryContract
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
