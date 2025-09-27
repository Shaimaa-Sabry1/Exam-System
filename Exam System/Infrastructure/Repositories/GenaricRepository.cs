using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Infrastructure.Repositories
{
    public class GenaricRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ExamDbcontext _dbcontext;
        private readonly DbSet<T> _dbSet;

        public GenaricRepository(ExamDbcontext dbcontext)
        {
            this._dbcontext = dbcontext;
            this._dbSet = dbcontext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T1>> GetAllAsync<T1>()
        {
            throw new NotImplementedException();
        }

        public  async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id).AsTask();
        }

        public Task<T> UpdateAsync(T entity)
        {
            _dbcontext.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
