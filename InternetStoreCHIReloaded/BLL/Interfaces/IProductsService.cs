using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductsService
    {
        Task<ResultContract> AddProductAsync(Product newCategory);
        Task<ResultContract> DeleteProductAsync(int id);
        Task<ResultContract<Product>> GetProductAsync(int id);
        Task<ResultContract<List<Product>>> GetProductsAsync();
        Task<ResultContract<Product>> UpdateProductAsync(Product newCategoryInfo);
    }
}
