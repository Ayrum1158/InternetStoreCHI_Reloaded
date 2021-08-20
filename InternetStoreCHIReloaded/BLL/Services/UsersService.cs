using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly IGenericRepository<UserEntity> _usersGenericRepository;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IAccessTokenGenerator _tokenGenerator;
        private readonly IGenericRepository<ProductEntity> _productGenericRepository;

        public UsersService(
            IMapper mapper,
            IUsersRepository usersRepository,
            IGenericRepository<UserEntity> usersGenericRepository,
            UserManager<UserEntity> userManager,
            IAccessTokenGenerator tokenGenerator,
            IGenericRepository<ProductEntity> productGenericRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _usersGenericRepository = usersGenericRepository;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _productGenericRepository = productGenericRepository;
        }

        public async Task<ServiceResult> RegisterUserAsync(UserRegistrationModel newUserModel)
        {
            ServiceResult result;

            if (newUserModel.Password != newUserModel.ConfirmPassword)
            {
                result = new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "Password and Confirm password do not match.",
                };
                return result;
            }

            var newDbUserModel = _mapper.Map<NewUserDbModel>(newUserModel);

            var dbResponse = await _usersRepository.RegisterUserAsync(newDbUserModel);

            result = _mapper.Map<ServiceResult>(dbResponse);

            return result;
        }

        public async Task<ServiceResult<string>> LoginUserAsync(UserLoggingInModel loginModel)
        {
            var result = new ServiceResult<string>();

            var userEntity = await _usersGenericRepository.FindFirstOrDefaultAsync(u => u.UserName == loginModel.Username);

            if (userEntity == null)
            {
                result.IsSuccessful = false;
                result.Message = "Check your login data.";
                return result;
            }

            if (await _userManager.CheckPasswordAsync(userEntity, loginModel.Password))
            {
                var user = _mapper.Map<User>(userEntity);

                result.IsSuccessful = true;
                result.Message = "User logged in successfuly!";
                result.Data = _tokenGenerator.GenerateToken(user);
                return result;
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Check your login data.";
                return result;
            }
        }

        public async Task<ServiceResult> AddToUserCart(int userId, AddToCartModel addToCartModel)// no user validation because we retrieve userId via JWT
        {
            if (addToCartModel.Quantity < 1)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "Quantity is less then 1."
                };
            }

            var isProductPresent = await _productGenericRepository.IsPresentInDbAsync(p => p.Id == addToCartModel.ProductId);
            if (!isProductPresent)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "No product found."
                };
            }

            var userEntity = await _usersRepository.GetUserWithCartAsync(userId);

            var user = _mapper.Map<User>(userEntity);

            bool userHasProductInCart = user.UserCart.CartItems.Any(ci => ci.ProductId == addToCartModel.ProductId);

            var dbModel = _mapper.Map<ProductToCartDbModel>(addToCartModel);

            var quantityDbResponse = await _usersRepository.GetQuantityOfProductInCartAsync(userId, addToCartModel.ProductId);
            dbModel.Quantity += quantityDbResponse.Data;
            var dbResponse = await _usersRepository.SetProductToUserCartAsync(userId, dbModel, userHasProductInCart);

            var result = _mapper.Map<ServiceResult>(dbResponse);

            if(result.IsSuccessful)
            {
                result.Message = "Product was successfuly added!";
            }

            return result;
        }

        public async Task<ServiceResult> RemoveFromUserCart(int userId, RemoveFromCartModel removeFromCartModel)// no user validation because we retrieve userId via JWT
        {
            if (removeFromCartModel.Quantity < 1)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "Quantity is less then 1."
                };
            }

            var isProductPresent = await _productGenericRepository.IsPresentInDbAsync(p => p.Id == removeFromCartModel.ProductId);

            if (!isProductPresent)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "No product found."
                };
            }

            var userEntity = await _usersRepository.GetUserWithCartAndProductsAsync(userId);

            var user = _mapper.Map<User>(userEntity);

            bool userHasProductInCart = user.UserCart.CartItems.Any(ci => ci.ProductId == removeFromCartModel.ProductId);

            DbResponse dbResponse;

            if (!userHasProductInCart)
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "There is none of this product in cart"
                };
            }

            var productQuantityInCart = user.UserCart.CartItems.Single(ci => ci.ProductId == removeFromCartModel.ProductId).Quantity;

            if (productQuantityInCart <= 1 || (productQuantityInCart - removeFromCartModel.Quantity) < 1)// removing Cartitems row in Db
            {
                dbResponse = await _usersRepository.RemoveCartItemFromUserCartAsync(userId, removeFromCartModel.ProductId);
            }
            else// setting lesser quantity
            {
                var dbModel = _mapper.Map<ProductToCartDbModel>(removeFromCartModel);
                dbModel.Quantity = productQuantityInCart - removeFromCartModel.Quantity;
                dbResponse = await _usersRepository.SetProductToUserCartAsync(userId, dbModel, true);
            }

            var result = _mapper.Map<ServiceResult>(dbResponse);

            return result;
        }

        public async Task<ServiceResult> MakeAnOrder(int userId)// no user validation because we retrieve userId via JWT
        {
            var dbResponse = await _usersRepository.MakeAnOrder(userId);

            var result = _mapper.Map<ServiceResult>(dbResponse);

            return result;
        }
    }
}
