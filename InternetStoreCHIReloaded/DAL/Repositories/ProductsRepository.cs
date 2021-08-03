using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductsRepository : GenericRepository<ProductEntity>, IProductsRepository
    {
        public ProductsRepository(StoreContext dbcontext) : base(dbcontext)
        { }

        public override async Task<bool> UpdateAsync(ProductEntity entity)
        {
            _fieldOfWork.Update(entity).Property(pe => pe.CreatedDate).IsModified = false;
            return await SaveAsync();
        }

        public async Task<IEnumerable<ProductEntity>> FindSortAndPaginateAll(
            Expression<Func<ProductEntity, bool>> whereExpr,
            Func<ProductEntity, object> sortByFunc,
            SortDirection sortDirection,
            int pageSize,
            int page)
        {
            IEnumerable<ProductEntity> products = null;

            var findTask = Task.Run(() =>
            {
                products = _fieldOfWork.Where(whereExpr);

                switch (sortDirection)
                {
                    case SortDirection.Ascending:
                        products = products.OrderBy(sortByFunc);
                        break;
                    case SortDirection.Descending:
                        products = products.OrderByDescending(sortByFunc);
                        break;
                    default:// SortHow.None
                        break;
                }

                int from = pageSize * (page - 1);
                products = products.Skip(from).Take(pageSize).ToList();
            });

            await findTask;

            return products;
        }
    }
}
