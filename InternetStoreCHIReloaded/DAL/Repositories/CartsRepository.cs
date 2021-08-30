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

        public async Task<bool> AddAsync(CartItemEntity cartEntity)
        {
            _dbContext.Cartitems.Add(cartEntity);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(CartItemEntity cartEntity)
        {
            _dbContext.Cartitems.Update(cartEntity);
            return await SaveAsync();
        }

        public async Task<CartItemEntity> GetItemInUserCartAsync(int cartId, int productId)
        {
            return await _dbContext.Cartitems.SingleOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task<int> GetAmountOfItemsInUserCartAsync(int cartId)
        {
            return await _dbContext.Carts.Include(c => c.CartItems).Where(c => c.Id == cartId).Select(c => c.CartItems.Count).SingleAsync();
        }

        public async Task<List<CartItemEntity>> GetAllItemsInUserCartAsync(int userId)
        {
            var cartId = await GetCartIdFromUserIdAsync(userId);
            var cartItems = await _dbContext.Cartitems.Include(ci => ci.Product).Where(ci => ci.CartId == cartId).ToListAsync();
            return cartItems;
        }

        public async Task<int> GetCartIdFromUserIdAsync(int userId)
        {
            return await _dbContext.Carts.Where(c => c.UserId == userId).Select(c => c.Id).SingleAsync();
        }

        public async Task<bool> IsUserCartEmptyAsync(int userId)
        {
            int cartId = await GetCartIdFromUserIdAsync(userId);
            return ! await _dbContext.Cartitems.AnyAsync(ci => ci.CartId == cartId);
        }

        public async Task<bool> RemoveCartItemFromUserCartAsync(CartItemEntity cartItem)// removes entry from ProductWithQuantity table
        {
            _dbContext.Cartitems.Remove(cartItem);
            return await SaveAsync();
        }
    }
}
