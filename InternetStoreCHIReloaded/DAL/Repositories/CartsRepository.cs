using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CartsRepository : GenericRepository<CartEntity>, ICartsRepository
    {
        public CartsRepository(
            StoreContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<List<CartItemEntity>> GetAllItemsInUserCartAsync(int userId)
        {
            var cartId = await GetCartIdFromUserIdAsync(userId);
            var cartItems = await _dbcontext.Cartitems.Include(ci => ci.Product).Where(ci => ci.CartId == cartId).ToListAsync();
            return cartItems;
        }

        public async Task<int> GetCartIdFromUserIdAsync(int userId)
        {
            return await _dbcontext.Carts.Where(c => c.UserId == userId).Select(c => c.Id).SingleAsync();
        }

        public async Task<bool> IsUserCartEmptyAsync(int userId)
        {
            int cartId = await GetCartIdFromUserIdAsync(userId);
            return ! await _dbcontext.Cartitems.AnyAsync(ci => ci.CartId == cartId);
        }

        public async Task<DbResponse> SetProductToUserCartAsync(int userId, ProductToCartDbModel productToCartDbModel, bool userHasProductInCart)
        {
            if (userHasProductInCart)
            {
                var product = _dbcontext.Cartitems.Where(ci => ci.ProductId == productToCartDbModel.ProductId).Single();
                product.Quantity = productToCartDbModel.Quantity;
                _dbcontext.Cartitems.Update(product);
            }
            else
            {
                var cartId = await GetCartIdFromUserIdAsync(userId);

                var newCartItem = new CartItemEntity()
                {
                    ProductId = productToCartDbModel.ProductId,
                    Quantity = productToCartDbModel.Quantity,
                    CartId = cartId
                };

                _dbcontext.Cartitems.Add(newCartItem);
            }

            bool saveSuccess = await SaveAsync();
            var dbResponse = new DbResponse()
            {
                IsSuccessful = saveSuccess
            };

            return dbResponse;
        }

        public async Task<int?> GetQuantityOfProductInCartAsync(int userId, int productId)
        {
            var cartId = await GetCartIdFromUserIdAsync(userId);
            var cartItem = await _dbcontext.Cartitems.SingleOrDefaultAsync(ci => ci.ProductId == productId && ci.CartId == cartId);

            int? result = null;
            if (cartItem != null)
            {
                result = cartItem.Quantity;
            }

            return result;
        }

        public async Task<DbResponse> RemoveCartItemFromUserCartAsync(int userId, int productId)// removes entry from ProductWithQuantity table
        {
            var cartId = await GetCartIdFromUserIdAsync(userId);
            var cartItem = await _dbcontext.Cartitems.SingleAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            _dbcontext.Cartitems.Remove(cartItem);

            bool success = await SaveAsync();
            var dbResponse = new DbResponse()
            {
                IsSuccessful = success
            };

            return dbResponse;
        }
    }
}
