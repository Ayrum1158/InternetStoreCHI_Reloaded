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
            var updatedEntity = new ProductEntity()
            {
                Id = entity.Id,
                CategoryId = entity.CategoryId,
                UpdatedDate = entity.UpdatedDate,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price
            };

            _fieldOfWork.Attach(updatedEntity).Property(e => e.CreatedDate).IsModified = false;
            var saveTask = SaveAsync();
            entity = updatedEntity;// if we will use referenced entity further
            return await saveTask;
        }
    }
}
