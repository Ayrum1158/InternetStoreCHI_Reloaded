using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoriesService
    {
        Task<ResultContract> AddCategoryAsync(Category newCategory);
        Task<ResultContract> DeleteCategoryAsync(int id);
        Task<ResultContract<Category>> GetCategoryAsync(int id);
        Task<ResultContract<List<Category>>> GetCategoriesAsync();
        Task<ResultContract<Category>> UpdateCategoryAsync(Category newCategoryInfo);
    }
}
