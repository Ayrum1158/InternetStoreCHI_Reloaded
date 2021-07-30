﻿using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductsService
    {
        Task<ServiceResult<Product>> AddProductAsync(Product newProduct);
        Task<ServiceResult> DeleteProductAsync(int id);
        Task<ServiceResult<Product>> GetProductAsync(int id);
        Task<ServiceResult<List<Product>>> GetProductsAsync();
        Task<ServiceResult<Product>> UpdateProductAsync(Product newProductInfo);
    }
}
