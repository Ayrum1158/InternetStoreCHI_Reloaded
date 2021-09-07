using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICartsService
    {
        Task<ServiceResult> AddToUserCartAsync(int userId, AddToCartModel addToCartModel);
        Task<ServiceResult> RemoveFromUserCartAsync(int userId, RemoveFromCartModel removeFromCartModel);
    }
}
