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

        public async Task<DbResponse> SetProductToUserCartAsync(int userId, ProductToCartDbModel productToCartDbModel, bool userHasProductInCart)
        {
            if (userHasProductInCart)
            {
                var product = _dbcontext.Cartitems.Where(ci => ci.ProductId == productToCartDbModel.ProductId).Single();
                product.Quantity = productToCartDbModel.Quantity;
            }
            else
            {
                var newCartItem = _dbcontext.Cartitems.Add(new CartItemEntity()
                {
                    ProductId = productToCartDbModel.ProductId,
                    Quantity = productToCartDbModel.Quantity
                });

                var user = _dbcontext.Users.Single(u => u.Id == userId);
                user.UserCart.CartItems.Add(newCartItem.Entity);
            }

            bool saveSuccess = await SaveAsync();

            var dbResponse = new DbResponse();

            if (saveSuccess)
            {
                dbResponse.IsSuccessful = true;
                dbResponse.Message = "Product was set successfully!";
            }
            else
            {
                dbResponse.IsSuccessful = false;
                dbResponse.Message = "Nothing has changed.";
            }

            return dbResponse;
        }

        public async Task<DbResponse> RemoveCartItemFromUserCartAsync(int userId, int productId)// removes entry from ProductWithQuantity table
        {
            var user = await GetUserWithCartAsync(userId);

            DbResponse dbResponse = new DbResponse();

            var productInCart = user.UserCart.CartItems.Where(p => p.ProductId == productId).FirstOrDefault();

            user.UserCart.CartItems.Remove(productInCart);
            _dbcontext.Cartitems.Remove(productInCart);// also remove cartItem entry

            bool success = await UpdateAsync(user);

            if (success)
            {
                dbResponse.IsSuccessful = true;
                dbResponse.Message = "Product entry was removed successfuly!";
            }
            else
            {
                dbResponse.IsSuccessful = false;
                dbResponse.Message = "Nothing has changed.";
            }

            return dbResponse;
        }

        public async Task<int?> GetQuantityOfProductInCartAsync(int userId, int productId)
        {
            var user = await GetUserWithCartAsync(userId);
            var product = user.UserCart.CartItems.FirstOrDefault(p => p.ProductId == productId);

            int? result = null;

            if (product != null)
            {
                result = product.Quantity;
            }

            return result;
        }

        public async Task<DbResponse> MakeAnOrder(int userId)
        {
            var user = await GetUserWithCartAndProductsAsync(userId);

            DbResponse dbResponse = new DbResponse();

            if (!user.UserCart.CartItems.Any())// if no items in cart
            {
                dbResponse.IsSuccessful = false;
                dbResponse.Message = "Cart is empty.";
                return dbResponse;
            }

            DateTime orderCreationdate = DateTime.UtcNow;

            var orderedProducts = user.UserCart.CartItems.Select(ci => new OrderedProductEntity()
            {
                Date = orderCreationdate,
                Price = ci.Product.Price,
                Product = ci.Product,
                Quantity = user.UserCart.CartItems.Where(i => i.ProductId == ci.ProductId).First().Quantity
            }).ToList();

            OrderEntity newOrder = new OrderEntity()
            {
                CreationDate = orderCreationdate,
                OrderItems = orderedProducts,
                TotalSum = orderedProducts.Sum(p => p.Price)
            };

            user.UserOrders.Add(newOrder);

            _dbcontext.Cartitems.RemoveRange(user.UserCart.CartItems);

            user.UserCart.CartItems.Clear();

            bool success = await SaveAsync();

            if (success)
            {
                dbResponse.IsSuccessful = true;
                dbResponse.Message = "Successfuly made order!";
            }
            else
            {
                dbResponse.IsSuccessful = false;
                dbResponse.Message = "Failed to make an order";
            }

            return dbResponse;
        }

        public async Task<UserEntity> GetUserWithCartAsync(int userId)
        {
            return await FindFirstOrDefaultAsync(u => u.Id == userId, u => u.UserCart, u => u.UserCart.CartItems);
        }

        public async Task<UserEntity> GetUserWithCartAndProductsAsync(int userId)
        {
            return await _dbcontext.Users
                .Include(u => u.UserCart)
                .ThenInclude(uc => uc.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync();
        }
    }
}
