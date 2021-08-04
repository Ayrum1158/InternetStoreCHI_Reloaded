using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductsRepository : IGenericRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> FindSortAndPaginateAll(
            ProductRequestFilter filter,
            int pageSize,
            int page);
    }
}
