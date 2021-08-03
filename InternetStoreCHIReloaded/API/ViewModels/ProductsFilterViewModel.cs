using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ProductsFilterViewModel
    {
        public int? CategoryId { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public string SortPropName { get; set; } = "Id";
        public SortDirection SortDirection { get; set; } = SortDirection.None;
    }
}
