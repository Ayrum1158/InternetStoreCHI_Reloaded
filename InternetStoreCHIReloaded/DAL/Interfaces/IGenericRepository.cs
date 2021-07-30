using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<DbResponse<T>> AddAsync(T entity);
        Task<IEnumerable<T>> FindAllAsync(Func<T, bool> predicate);
        Task<T> FindFirstOrDefaultAsync(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        bool IsPresentInDbAsync(Func<T, bool> predicate);
        Task<bool> RemoveAsync(int entityId);
        Task<bool> UpdateAsync(T entity);
    }
}
