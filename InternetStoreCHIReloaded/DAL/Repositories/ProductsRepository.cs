using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using DAL.Models;
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
            _dbcontext.Products.Update(entity).Property(pe => pe.CreatedDate).IsModified = false;
            return await SaveAsync();
        }

        public async Task<IEnumerable<ProductEntity>> FindSortAndPaginateAll(
            ProductRequestFilter filter,
            int pageSize,
            int page)
        {
            var productsIQ = _dbcontext.Products.AsQueryable();

            if (filter.CategoryId != null)
                productsIQ = productsIQ.Where((pe) => pe.CategoryId == filter.CategoryId);
            if (filter.FromPrice != null)
                productsIQ = productsIQ.Where((pe) => pe.Price >= filter.FromPrice);
            if (filter.ToPrice != null)
                productsIQ = productsIQ.Where((pe) => pe.Price <= filter.ToPrice);

            var orderByExpr = GetOrderByExpression(filter.OrderByProperty);

            switch (filter.SortDirection)
            {
                case SortDirection.Ascending:
                    productsIQ = productsIQ.OrderBy(orderByExpr).ThenBy(pe => pe.Id);
                    break;
                case SortDirection.Descending:
                    productsIQ = productsIQ.OrderByDescending(orderByExpr).ThenBy(pe => pe.Id);
                    break;
                default:// SortDirection.None
                    productsIQ = productsIQ.OrderBy(pe => pe.Id);
                    break;
            }

            int from = pageSize * (page - 1);

            var products = await productsIQ.Skip(from).Take(pageSize).ToListAsync();

            return products;
        }

        private Expression<Func<ProductEntity, object>> GetOrderByExpression(OrderByProperty orderByProp)
        {
            string propStringName = Enum.GetName(typeof(OrderByProperty), orderByProp) ?? "Id";

            var type = typeof(ProductEntity);
            var prop = type.GetProperty(propStringName);
            var param = Expression.Parameter(type);
            var expr = Expression.Lambda<Func<ProductEntity, object>>(Expression.Convert(Expression.Property(param, prop), typeof(object)), param);

            return expr;
        }
    }
}
