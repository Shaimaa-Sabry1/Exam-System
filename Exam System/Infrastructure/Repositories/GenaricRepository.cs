using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Infrastructure.Repositories
{
    public class GenaricRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ExamDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;

        public GenaricRepository(ExamDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
            this._dbSet = dbcontext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbcontext.Remove(entity);
            await Task.CompletedTask;
        }

        

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _dbSet.CountAsync();
            var items = await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
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
