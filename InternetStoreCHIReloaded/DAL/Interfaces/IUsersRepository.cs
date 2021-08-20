using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<DbResponse> RegisterUserAsync(NewUserDbModel newUser);
        Task<DbResponse> SetProductToUserCartAsync(int userId, ProductToCartDbModel productToCartDbModel, bool userHasProductInCart);
        Task<DbResponse> RemoveCartItemFromUserCartAsync(int userId, int productId);
        Task<DbResponse<int>> GetQuantityOfProductInCartAsync(int userId, int productId);
        Task<DbResponse> MakeAnOrder(int userId);
        Task<UserEntity> GetUserWithCartAsync(int userId);
        Task<UserEntity> GetUserWithCartAndProductsAsync(int userId);

    }
}
