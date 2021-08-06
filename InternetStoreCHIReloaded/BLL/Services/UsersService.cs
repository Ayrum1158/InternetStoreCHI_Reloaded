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

        public UsersService(
            IMapper mapper,
            IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
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
    }
}
