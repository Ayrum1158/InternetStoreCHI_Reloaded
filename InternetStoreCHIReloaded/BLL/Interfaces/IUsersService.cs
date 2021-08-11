﻿using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUsersService
    {
        Task<ServiceResult> RegisterUserAsync(UserRegistrationModel newUser);
        Task<ServiceResult<string>> LoginUserAsync(UserLoggingInModel userModel);
        Task<ServiceResult> AddToUserCart(int userId, AddToCartModel atcModel);
    }
}
