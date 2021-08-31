using AutoMapper;
using BLL.ConfigPOCOs;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Options;
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
        private readonly CartsConfig _cartsConfig;

        public CartsService(IMapper mapper,
            IGenericRepository<ProductEntity> productGenericRepository,
            ICartsRepository cartsRepository,
            IOptionsMonitor<CartsConfig> cartsConfig)
        {
            _mapper = mapper;
            _productGenericRepository = productGenericRepository;
            _cartsRepository = cartsRepository;
            _cartsConfig = cartsConfig.CurrentValue;
        }

        public async Task<ServiceResult> AddToUserCartAsync(int userId, AddToCartModel addToCartModel)// no user validation because we retrieve userId via JWT
        {
            var validationResult = await ValidateModelAsync(addToCartModel);
            if (!validationResult.IsSuccessful)
                return validationResult;

            int cartId = await _cartsRepository.GetCartIdFromUserIdAsync(userId);
            var cartItemEntity = await _cartsRepository.GetItemInUserCartAsync(cartId, addToCartModel.ProductId);
            bool userHasProductInCart = cartItemEntity != null;

            int futureQuantity = addToCartModel.Quantity;
            if (userHasProductInCart)
                futureQuantity += cartItemEntity.Quantity;

            var businessRulesCheckResult = await CheckBusinessRulesAsync(cartId, futureQuantity);
            if (!businessRulesCheckResult.IsSuccessful)
                return businessRulesCheckResult;

            bool success;
            if (userHasProductInCart)// updating with more quantity
            {
                cartItemEntity.Quantity = futureQuantity;
                success = await _cartsRepository.UpdateAsync(cartItemEntity);
            }
            else// adding new one
            {
                var cartItem = new CartItem()
                {
                    ProductId = addToCartModel.ProductId,
                    Quantity = futureQuantity
                };
                var newCartItemEntity = _mapper.Map<CartItemEntity>(cartItem);
                newCartItemEntity.CartId = cartId;
                success = await _cartsRepository.AddAsync(newCartItemEntity);
            }
            var result = new ServiceResult() { IsSuccessful = success };
            if (result.IsSuccessful)
            {
                result.Message = "Product(s) added successfully!";
            }
            return result;
        }

        public async Task<ServiceResult> RemoveFromUserCart(int userId, RemoveFromCartModel removeFromCartModel)// no user validation because we retrieve userId via JWT
        {
            var validationResult = await ValidateModelAsync(removeFromCartModel);
            if (!validationResult.IsSuccessful)
                return validationResult;

            int cartId = await _cartsRepository.GetCartIdFromUserIdAsync(userId);
            var cartItemEntity = await _cartsRepository.GetItemInUserCartAsync(cartId, removeFromCartModel.ProductId);
            if (cartItemEntity == null)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "No such product in cart"
                };
            }
            int itemQuantityInCart = cartItemEntity.Quantity;
            bool removeRowConditionResult = itemQuantityInCart <= 1 || (itemQuantityInCart - removeFromCartModel.Quantity) < 1;
            bool success;
            if (removeRowConditionResult)// removing CartItems row in Db
            {
                success = await _cartsRepository.RemoveCartItemFromUserCartAsync(cartItemEntity);
            }
            else// setting lesser quantity
            {
                cartItemEntity.Quantity -= removeFromCartModel.Quantity;
                success = await _cartsRepository.UpdateAsync(cartItemEntity);
            }
            var result = new ServiceResult() { IsSuccessful = success };
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

        private async Task<ServiceResult> CheckBusinessRulesAsync(int cartId, int futureQuantity)
        {
            if (futureQuantity > _cartsConfig.MaximumItemQuantity)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = $"Quantity of one individual item cannot exceed {_cartsConfig.MaximumItemQuantity}."
                };
            }
            int itemsInCart = await _cartsRepository.GetAmountOfItemsInUserCartAsync(cartId);
            if (itemsInCart >= _cartsConfig.MaximumItemsInCart)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = $"Amount of individual items in cart cannot exceed {_cartsConfig.MaximumItemsInCart}."
                };
            }
            return new ServiceResult() { IsSuccessful = true };
        }
    }
}
