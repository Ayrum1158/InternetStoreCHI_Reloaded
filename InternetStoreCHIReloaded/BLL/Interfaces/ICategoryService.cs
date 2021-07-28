using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        ResultContract AddCategory(Category newCategory);
        public ResultContract DeleteCategory(int id);
        ResultContract<Category> GetCategory(int id);
        ResultContract<List<Category>> GetCategories();
        ResultContract<Category> UpdateCategory(Category newCategoryInfo);
    }
}
