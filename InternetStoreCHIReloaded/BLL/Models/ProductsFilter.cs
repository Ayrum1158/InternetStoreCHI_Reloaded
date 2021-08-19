using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class ProductsFilter
    {
        public int? CategoryId { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public OrderByProperty OrderByProperty { get; set; } = OrderByProperty.Id;
        public SortDirection SortDirection { get; set; } = SortDirection.None;
    }
}
