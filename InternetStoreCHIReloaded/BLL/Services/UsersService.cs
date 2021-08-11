using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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

            if(newUserModel.Password != newUserModel.ConfirmPassword)
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

            if(userEntity == null)
            {
                result.IsSuccessful = false;
                result.Message = "Check your login data.";
                return result;
            }

            if(await _userManager.CheckPasswordAsync(userEntity, loginModel.Password))
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

        public async Task<ServiceResult> AddToUserCart(int userId, AddToCartModel atcModel)// no user validation because we retrieve userId via JWT
        {
            var isProductPresent = await _productGenericRepository.IsPresentInDbAsync(p => p.Id == atcModel.ProductId);

            if (isProductPresent)
            {
                var dbModel = _mapper.Map<AddToCartDbModel>(atcModel);
                var dbResponse = await _usersRepository.AddProductToUserCartAsync(userId, dbModel);

                var result = _mapper.Map<ServiceResult>(dbResponse);
                return result;
            }
            else
            {
                var result = new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "No product found."
                };
                return result;
            }
        }
    }
}
