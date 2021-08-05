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
            _fieldOfWork.Update(entity).Property(pe => pe.CreatedDate).IsModified = false;
            return await SaveAsync();
        }

        public async Task<IEnumerable<ProductEntity>> FindSortAndPaginateAll(
            ProductRequestFilter filter,
            int pageSize,
            int page)
        {
            var productsIQ = _fieldOfWork.AsQueryable();

            if (filter.CategoryId != null)
                productsIQ = productsIQ.Where((pe) => pe.CategoryId == filter.CategoryId);
            if (filter.FromPrice != null)
                productsIQ = productsIQ.Where((pe) => pe.Price >= filter.FromPrice);
            if (filter.ToPrice != null)
                productsIQ = productsIQ.Where((pe) => pe.Price <= filter.ToPrice);

            var prop = typeof(ProductEntity).GetProperty(filter.SortPropName);
            Func<ProductEntity, object> sortByFunc = p => prop.GetValue(p, null);// func to get property

            List<ProductEntity> products = null;

            Task queryTask = Task.Run(()=> { });// don't want to catch exception on next await, so empty task

            switch (filter.SortDirection)
            {
                case SortDirection.Ascending:
                    queryTask = Task.Run(() => products = productsIQ.OrderBy(sortByFunc).ToList());
                    break;
                case SortDirection.Descending:
                    queryTask = Task.Run(() => products = productsIQ.OrderByDescending(sortByFunc).ToList());
                    break;
                default:// SortDirection.None
                    products = await productsIQ.ToListAsync();
                    break;
            }

            int from = pageSize * (page - 1);

            await queryTask;

            products = products.Skip(from).Take(pageSize).ToList();

            return products;
        }
    }
}
