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
        private readonly SignInManager<UserEntity> _signInManager;

        public UsersService(
            IMapper mapper,
            IUsersRepository usersRepository,
            SignInManager<UserEntity> signInManager)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _signInManager = signInManager;
        }

        public async Task<ServiceResult> RegisterUserAsync(UserRegistrationModel newUserModel)
        {
            ServiceResult result = null;

            if(newUserModel.Password != newUserModel.ConfirmPassword)
            {
                result = new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "Password and Confirm password do not match.",
                };
                return result;
            }

            var newDbUserModel = _mapper.Map<NewDbUserModel>(newUserModel);

            var dbResponse = await _usersRepository.RegisterUserAsync(newDbUserModel);

            result = _mapper.Map<ServiceResult>(dbResponse);

            return result;
        }

        public async Task<ServiceResult> LoginUserAsync(UserLoggingInModel loginModel)
        {
            var result = new ServiceResult();

            var loginResult = await _signInManager.PasswordSignInAsync(
                    loginModel.Username,
                    loginModel.Password,
                    loginModel.RememberMe,
                    false);

            if (loginResult.Succeeded)
            {
                result.IsSuccessful = true;
                result.Message = "User successfuly logged in!";
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Failed to log in.";
            }

            return result;
        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
