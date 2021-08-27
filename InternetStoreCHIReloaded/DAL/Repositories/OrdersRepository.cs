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
    public class OrdersRepository : GenericRepository<OrderEntity>, IOrdersRepository
    {
        public OrdersRepository(
            StoreContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<DbResponse> MakeAnOrder(List<CartItemEntity> cartItemsToRemove, OrderEntity order)
        {
            _dbcontext.Cartitems.RemoveRange(cartItemsToRemove);
            _dbcontext.Orders.Add(order);
            bool success = await SaveAsync();
            DbResponse dbResponse = new DbResponse()
            {
                IsSuccessful = success
            };
            return dbResponse;
        }
    }
}
