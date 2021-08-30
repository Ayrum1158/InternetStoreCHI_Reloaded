using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICartsService
    {
        Task<ServiceResult> AddToUserCart(int userId, AddToCartModel addToCartModel);
        Task<ServiceResult> RemoveFromUserCart(int userId, RemoveFromCartModel removeFromCartModel);
    }
}
