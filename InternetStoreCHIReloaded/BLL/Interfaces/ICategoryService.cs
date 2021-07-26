using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        ResultContract AddCategory(CategoryContract newCategory);
        ResultContract UpdateCategory(CategoryContract newCategoryInfo);
    }
}
