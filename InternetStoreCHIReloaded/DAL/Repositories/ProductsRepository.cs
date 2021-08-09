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

            var sortByFunc = GetProperty(filter.OrderByProperty);

            var products = await productsIQ.ToListAsync();

            switch (filter.SortDirection)
            {
                case SortDirection.Ascending:
                    products = products.OrderBy(sortByFunc).ThenBy(pe => pe.Id).ToList();
                    break;
                case SortDirection.Descending:
                    products = products.OrderByDescending(sortByFunc).ThenBy(pe => pe.Id).ToList();
                    break;
                default:// SortDirection.None
                    products = products.OrderBy(pe => pe.Id).ToList();
                    break;
            }

            int from = pageSize * (page - 1);

            products = products.Skip(from).Take(pageSize).ToList();

            return products;
        }

        private Func<ProductEntity, object> GetProperty(OrderByProperty orderByProp)
        {
            string propStringName;

            switch (orderByProp)
            {
                case OrderByProperty.Id:
                    propStringName = "Id";
                    break;
                case OrderByProperty.Name:
                    propStringName = "Name";
                    break;
                case OrderByProperty.Price:
                    propStringName = "Price";
                    break;
                default:
                    propStringName = "Id";
                    break;
            }

            var prop = typeof(ProductEntity).GetProperty(propStringName);

            Func<ProductEntity, object> sortByFunc = p => prop.GetValue(p, null);// func to get property
            return sortByFunc;
        }
    }
}
