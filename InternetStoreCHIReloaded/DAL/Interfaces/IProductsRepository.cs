using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IProductsRepository : IGenericRepository<ProductEntity>
    {
    }
}
