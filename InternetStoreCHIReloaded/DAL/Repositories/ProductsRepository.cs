using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductsRepository : GenericRepository<ProductEntity>, IProductsRepository
    {
        public ProductsRepository(StoreContext dbcontext) : base(dbcontext)
        {
        }

        public override async Task<bool> UpdateAsync(ProductEntity entity)
        {
            _fieldOfWork.Update(entity).Property(pe => pe.CreatedDate).IsModified = false;
            return await SaveAsync();
        }
    }
}
