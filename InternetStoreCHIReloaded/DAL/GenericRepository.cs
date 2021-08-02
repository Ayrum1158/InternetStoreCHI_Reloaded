using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly StoreContext _dbcontext;
        private DbSet<T> _fieldOfWork;

        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;

            _fieldOfWork = dbcontext.Set<T>();
        }

        public async Task<DbResponse<T>> AddAsync(T entity)
        {
            _fieldOfWork.Add(entity);// don't think we need to use AddAsync

            bool success = await SaveAsync();

            return new DbResponse<T>()
            {
                IsSuccessful = success,
                Data = entity
            };
        }

        public async Task<IEnumerable<T>> FindAllAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => _fieldOfWork.Where(predicate).ToList());
        }

        public async Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _fieldOfWork.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _fieldOfWork.ToListAsync();
        }

        public async Task<bool> IsPresentInDbAsync(Expression<Func<T, bool>> expression)
        {
            var res = (await FindFirstOrDefaultAsync(expression)) != null;
            return res;
        }

        public async Task<bool> RemoveAsync(int entityId)
        {
            T entity = new T();
            entity.Id = entityId;

            _fieldOfWork.Attach(entity);
            _fieldOfWork.Remove(entity);

            bool success = true;
            try
            {
                success = await SaveAsync();
            }
            catch (DbUpdateConcurrencyException)// when trying to delete with id not in Db
            {
                success = false;
            }

            return success;
        }

        private async Task<bool> SaveAsync()
        {
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _fieldOfWork.Update(entity);

            return await SaveAsync();
        }
    }
}
