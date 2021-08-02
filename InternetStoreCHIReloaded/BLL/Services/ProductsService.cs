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

        public async Task<ServiceResult<Product>> AddProductAsync(Product newProduct)
        {
            var result = new ServiceResult<Product>();

            if(IsValid(newProduct))
            {
                if(await _productRepository.IsPresentInDbAsync(pe => pe.Name == newProduct.Name))
                {
                    result.IsSuccessful = false;
                    result.Message = "Product with this name already exists";
                    return result;
                }

                newProduct.CreatedDate = newProduct.UpdatedDate = DateTime.UtcNow;

                var entity = _mapper.Map<ProductEntity>(newProduct);

                var dbResponse = await _productRepository.AddAsync(entity);

                result.IsSuccessful = dbResponse.IsSuccessful;

                if(dbResponse.IsSuccessful)
                {
                    result.Message = "Product was added successfully!";
                    result.Data = _mapper.Map<Product>(entity);
                }
                else
                {
                    result.Message = "No changes were made.";
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Fields are not proper.";
            }

            return result;
        }

        public async Task<ServiceResult> DeleteProductAsync(int id)
        {
            bool success = await _productRepository.RemoveAsync(id);

            var result = new ServiceResult() { IsSuccessful = success };

            if(success)
            {
                result.Message = "Product delete successful!";
            }
            else
            {
                result.Message = "No changes were made.";
            }

            return result;
        }

        public async Task<ServiceResult<Product>> GetProductAsync(int id)
        {
            var product = await _productRepository.FindFirstOrDefaultAsync(pe => pe.Id == id);

            var result = new ServiceResult<Product>();

            if(product != null)
            {
                result.IsSuccessful = true;
                result.Message = "Product retrieval success!";
                result.Data = _mapper.Map<Product>(product);
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Product was not found.";
            }

            return result;
        }

        public async Task<ServiceResult<List<Product>>> GetProductsAsync()// make it use pagination
        {
            var products = await _productRepository.GetAllAsync();

            var result = new ServiceResult<List<Product>>()
            {
                IsSuccessful = true,
                Data = _mapper.Map<List<Product>>(products),
                Message = "Categories retrieval successful!"
            };

            return result;
        }

        public async Task<ServiceResult<Product>> UpdateProductAsync(Product newProductInfo)
        {
            var result = new ServiceResult<Product>();

            if(IsValid(newProductInfo))
            {
                if (await _productRepository.IsPresentInDbAsync(pe => pe.Name == newProductInfo.Name))
                {
                    result.IsSuccessful = false;
                    result.Message = "Product with this name already exists.";
                    return result;
                }

                newProductInfo.UpdatedDate = DateTime.UtcNow;

                var productEntity = _mapper.Map<ProductEntity>(newProductInfo);

                bool success = await _productRepository.UpdateAsync(productEntity);

                if(success)
                {
                    result.IsSuccessful = true;
                    result.Message = "Product update successful!";
                    result.Data = newProductInfo;
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No changes were made.";
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Fields are not proper.";
            }

            return result;
        }
    }
}
