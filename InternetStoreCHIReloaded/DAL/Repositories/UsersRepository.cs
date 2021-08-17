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

        public async Task<DbResponse> SetProductToUserCartAsync(int userId, ProductToCartDbModel productToCartDbModel)
        {
            var user = await GetUserWithCartAsync(userId);

            if (user.UserCart.CartItems.Any(ci => ci.ProductId == productToCartDbModel.ProductId))
            {
                var product = user.UserCart.CartItems.Where(ci => ci.ProductId == productToCartDbModel.ProductId).Single();
                product.Quantity = productToCartDbModel.Quantity;
            }
            else
            {
                user.UserCart.CartItems.Add(new ProductWithQuantityEntity()
                {
                    ProductId = productToCartDbModel.ProductId,
                    Quantity = productToCartDbModel.Quantity,
                });
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

        public async Task<DbResponse> RemoveProductSetFromUserCartAsync(int userId, int productId)// removes entry from ProductWithQuantity table
        {
            var user = await GetUserWithCartAsync(userId);

            DbResponse dbResponse = new DbResponse();

            var productInCart = user.UserCart.CartItems.Where(p => p.ProductId == productId).FirstOrDefault();
            if (productInCart != null)
            {
                user.UserCart.CartItems.Remove(productInCart);
                _dbcontext.ProductsWithQuantity.Remove(productInCart);// also remove pwq entry

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
            }
            else
            {
                dbResponse.IsSuccessful = false;
                dbResponse.Message = "Product was not found in cart";
            }

            return dbResponse;
        }

        public async Task<DbResponse<int>> GetQuantityOfProductInCartAsync(int userId, int productId)
        {
            var user = await GetUserWithCartAsync(userId);
            var product = user.UserCart.CartItems.Where(p => p.ProductId == productId).FirstOrDefault();

            var dbResponse = new DbResponse<int>();

            if (product != null)
            {
                dbResponse.IsSuccessful = true;
                dbResponse.Message = "Product quantity retrieval success!";
                dbResponse.Data = product.Quantity;
            }
            else
            {
                dbResponse.IsSuccessful = false;
                dbResponse.Message = "Product was not found in cart";
            }

            return dbResponse;
        }

        public async Task<DbResponse> MakeAnOrder(int userId)
        {
            var user = await GetUserWithCartAsync(userId);

            DbResponse dbResponse = new DbResponse();

            if (!user.UserCart.CartItems.Any())// if no items in cart
            {
                dbResponse.IsSuccessful = false;
                dbResponse.Message = "Cart is empty.";
                return dbResponse;
            }

            // getting products only because can't include the product entities in user cart
            var productIds = user.UserCart.CartItems.Select(i => i.Id).ToList();

            var productsWithQuantity = await _dbcontext.ProductsWithQuantity.Where(p => productIds.Contains(p.Id)).Include(p => p.Product).ToListAsync();

            DateTime orderCreationdate = DateTime.UtcNow;

            var orderedProducts = productsWithQuantity.Select(pwq => new OrderedProductEntity()
            {
                OrderedDate = orderCreationdate,
                OrderedPrice = pwq.Product.Price,
                Product = pwq.Product,
                Quantity = user.UserCart.CartItems.Where(i => i.ProductId == pwq.ProductId).First().Quantity
            }).ToList();

            OrderEntity newOrder = new OrderEntity()
            {
                CreationDate = orderCreationdate,
                OrderItems = orderedProducts,
                TotalSum = orderedProducts.Sum(p => p.OrderedPrice)
            };

            user.UserOrders.Add(newOrder);

            _dbcontext.ProductsWithQuantity.RemoveRange(productsWithQuantity);

            user.UserCart.CartItems.Clear();

            bool success = await SaveAsync();

            if(success)
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

        private async Task<UserEntity> GetUserWithCartAsync(int userId)
        {
            return await FindFirstOrDefaultAsync(u => u.Id == userId, u => u.UserCart, u => u.UserCart.CartItems);
        }
    }
}
