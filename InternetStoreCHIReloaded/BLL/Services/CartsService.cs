using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CartsService : ICartsService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductEntity> _productGenericRepository;
        private readonly ICartsRepository _cartsRepository;

        public CartsService(IMapper mapper,
            IGenericRepository<ProductEntity> productGenericRepository,
            ICartsRepository cartsRepository)
        {
            _mapper = mapper;
            _productGenericRepository = productGenericRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task<ServiceResult> AddToUserCart(int userId, AddToCartModel addToCartModel)// no user validation because we retrieve userId via JWT
        {
            var validationResult = await ValidateModelAsync(addToCartModel);
            if (!validationResult.IsSuccessful)
                return validationResult;

            var quantityDbResponse = await _cartsRepository.GetQuantityOfProductInCartAsync(userId, addToCartModel.ProductId);

            bool userHasProductInCart = true;
            if (quantityDbResponse == null)
                userHasProductInCart = false;

            var dbModel = _mapper.Map<ProductToCartDbModel>(addToCartModel);
            dbModel.Quantity += quantityDbResponse ?? 0;
            var dbResponse = await _cartsRepository.SetProductToUserCartAsync(userId, dbModel, userHasProductInCart);
            var result = _mapper.Map<ServiceResult>(dbResponse);
            if (result.IsSuccessful)
            {
                result.Message = "Product was successfully added!";
            }
            return result;
        }

        public async Task<ServiceResult> RemoveFromUserCart(int userId, RemoveFromCartModel removeFromCartModel)// no user validation because we retrieve userId via JWT
        {
            var validationResult = await ValidateModelAsync(removeFromCartModel);
            if (!validationResult.IsSuccessful)
                return validationResult;

            int? quantityDbResponse = await _cartsRepository.GetQuantityOfProductInCartAsync(userId, removeFromCartModel.ProductId);
            if (quantityDbResponse == null)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "There is none of this product in cart."
                };
            }
            int productQuantityInCart = (int)quantityDbResponse;
            bool removeRowConditionResult = productQuantityInCart <= 1 || (productQuantityInCart - removeFromCartModel.Quantity) < 1;
            DbResponse dbResponse;
            if (removeRowConditionResult)// removing CartItems row in Db
            {
                dbResponse = await _cartsRepository.RemoveCartItemFromUserCartAsync(userId, removeFromCartModel.ProductId);
            }
            else// setting lesser quantity
            {
                var dbModel = _mapper.Map<ProductToCartDbModel>(removeFromCartModel);
                dbModel.Quantity = productQuantityInCart - removeFromCartModel.Quantity;
                dbResponse = await _cartsRepository.SetProductToUserCartAsync(userId, dbModel, true);
            }
            var result = _mapper.Map<ServiceResult>(dbResponse);
            if (result.IsSuccessful)
                result.Message = "Product quantity removed successfully!";

            return result;
        }

        private async Task<ServiceResult> ValidateModelAsync(ICartServiceValidatable model)
        {
            if (model.Quantity < 1)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "Quantity is less then 1."
                };
            }

            var isProductPresent = await _productGenericRepository.IsPresentInDbAsync(p => p.Id == model.ProductId);
            if (!isProductPresent)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "No product found."
                };
            }

            return new ServiceResult() { IsSuccessful = true };
        }
    }
}
