using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : IHasId
    {
        Task<DbResponse<T>> AddAsync(T entity);
        Task<IEnumerable<T>> FindAllAsync(Func<T, bool> predicate);
        Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> IsPresentInDbAsync(Expression<Func<T, bool>> expression);
        Task<bool> RemoveAsync(int entityId);
        Task<bool> UpdateAsync(T entity);
    }
}
