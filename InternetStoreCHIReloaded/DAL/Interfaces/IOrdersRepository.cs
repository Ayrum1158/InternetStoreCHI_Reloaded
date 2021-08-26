using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrdersRepository
    {
        Task<DbResponse> MakeAnOrder(int userId, OrderEntity order);
    }
}
