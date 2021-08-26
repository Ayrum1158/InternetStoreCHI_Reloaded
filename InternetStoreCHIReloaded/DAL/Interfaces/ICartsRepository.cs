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
        Task<DbResponse> SetProductToUserCartAsync(int userId, ProductToCartDbModel productToCartDbModel, bool userHasProductInCart);
        Task<DbResponse> RemoveCartItemFromUserCartAsync(int userId, int productId);
        Task<int?> GetQuantityOfProductInCartAsync(int userId, int productId);
    }
}
