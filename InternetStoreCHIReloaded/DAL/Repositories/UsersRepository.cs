using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UsersRepository : GenericRepository<UserEntity>, IUsersRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public UsersRepository(
            UserManager<UserEntity> userManager,
            IMapper mapper,

            StoreContext dbcontext) : base(dbcontext)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<DbResponse> RegisterUserAsync(NewUserDbModel newUser)
        {
            var userEntity = _mapper.Map<UserEntity>(newUser);
            var userCreateResult = await _userManager.CreateAsync(userEntity, newUser.Password);
            var response = new DbResponse();
            if (userCreateResult.Succeeded)
            {
                userEntity.UserCart = new CartEntity() { UserId = userEntity.Id };
                await SaveAsync();

                response.IsSuccessful = true;
                response.Message = "User registration success!";
                return response;
            }
            else
            {
                response.IsSuccessful = false;

                response.Message = "Errors occured while creating user:";
                foreach (var msg in userCreateResult.Errors)
                {
                    response.Message += $"\n{msg.Description}";
                }

                return response;
            }
        }
    }
}
