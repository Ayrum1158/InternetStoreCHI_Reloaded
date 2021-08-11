using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UsersRepository : GenericRepository<UserEntity>, IUsersRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IMapper _mapper;


        public UsersRepository(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IMapper mapper,

            StoreContext dbcontext) : base(dbcontext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<DbResponse> RegisterUserAsync(NewUserDbModel newUser)
        {
            var userEntity = _mapper.Map<UserEntity>(newUser);

            var userCreateResult = await _userManager.CreateAsync(userEntity, newUser.Password);

            var response = new DbResponse();

            if (userCreateResult.Succeeded)
            {
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

        public async Task<DbResponse> AddProductToUserCartAsync(int userId, AddToCartDbModel dbModel)
        {
            var dbResponse = new DbResponse();

            var user = await FindFirstOrDefaultAsync(u => u.Id == userId);

            user.UserCart.Products.Add(new ProductWithQuantityEntity()
            {
                ProductId = dbModel.ProductId,
                Quantity = dbModel.Quantity
            });

            bool saveSuccess = await SaveAsync();

            if (saveSuccess)
            {
                dbResponse.IsSuccessful = true;
                dbResponse.Message = "Product added successfully!";
            }
            else
            {
                dbResponse.IsSuccessful = false;
                dbResponse.Message = "Nothing has changed.";
            }

            return dbResponse;
        }
    }
}
