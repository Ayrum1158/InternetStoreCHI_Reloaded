using AutoMapper;
using BLL.Contracts;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IGenericRepository<ProductEntity> _productRepository;
        private readonly IMapper _mapper;

        public ProductsService(IGenericRepository<ProductEntity> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        private bool IsValid(Product product)
        {
            bool valid = true;

            valid = valid && product.Price >= 0;

            Regex reg;

            reg = new Regex("^[^ ][a-zA-Z ]{3,50}");
            valid = valid && reg.IsMatch(product.Name);

            reg = new Regex("^[^ ][a-zA-Z0-9. -]{3,500}");
            valid = valid && reg.IsMatch(product.Description);

            return valid;
        }

        public Task<ResultContract> AddProductAsync(Product newCategory)
        {
            throw new NotImplementedException();
        }

        public Task<ResultContract> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultContract<Product>> GetProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultContract<List<Product>>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultContract<Product>> UpdateProductAsync(Product newCategoryInfo)
        {
            throw new NotImplementedException();
        }
    }
}
