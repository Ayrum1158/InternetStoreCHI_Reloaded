using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICartsRepository
    {
        Task<int> GetCartIdFromUserIdAsync(int userId);
        Task<List<CartItemEntity>> GetAllItemsInUserCartAsync(int userId);
        Task<bool> IsUserCartEmptyAsync(int userId);
        Task<bool> RemoveCartItemFromUserCartAsync(CartItemEntity cartItem);
        Task<bool> AddAsync(CartItemEntity cartEntity);
        Task<bool> UpdateAsync(CartItemEntity cartEntity);
        Task<CartItemEntity> GetItemInUserCartAsync(int cartId, int productId);
    }
}
