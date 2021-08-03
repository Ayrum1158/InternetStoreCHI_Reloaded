using DAL.Entities;
using DAL.Enums;
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
            Expression<Func<ProductEntity, bool>> whereExpr,
            Func<ProductEntity, object> sortByFunc,
            SortDirection sortDirection,
            int pageSize,
            int page);
    }
}
