using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        //public CategoryContract ToCategoryContract()
        //{
        //    return new CategoryContract()
        //    {
        //        Id = CategoryId,
        //        CategoryName = CategoryName,
        //        CategoryDescription = CategoryDescription
        //    };
        //}
    }
}
