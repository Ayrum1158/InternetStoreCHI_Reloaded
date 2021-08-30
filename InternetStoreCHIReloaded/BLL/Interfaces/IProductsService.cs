using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductsService
    {
        Task<ServiceResult<Product>> AddProductAsync(Product product);
        Task<ServiceResult> DeleteProductAsync(int id);
        Task<ServiceResult<Product>> GetProductAsync(int id);
        Task<ServiceResult<List<Product>>> GetProductsAsync();
        Task<ServiceResult<List<Product>>> GetProductsFilteredAsync(
            int pageSize,
            int page,
            ProductsFilter filter);
        Task<ServiceResult<Product>> UpdateProductAsync(Product product);
    }
}
