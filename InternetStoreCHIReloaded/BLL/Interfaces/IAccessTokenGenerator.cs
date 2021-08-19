using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IAccessTokenGenerator
    {
        string GenerateToken(User user);
    }
}
