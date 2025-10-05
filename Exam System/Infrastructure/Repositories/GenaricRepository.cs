using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Infrastructure.Repositories
{
    public class GenaricRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ExamDbContext _dbcontext;
        //protected readonly DbSet<T> _dbSet;

        public GenaricRepository(ExamDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
            //this._dbSet = dbcontext.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbcontext.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbcontext.Remove(entity);
            await Task.CompletedTask;
        }


        public async Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _dbcontext.Set<T>().CountAsync();
            var items = await _dbcontext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }

        public async Task<IQueryable<Exam>> GetAllExamAsync()
        {
            var today = DateTime.Today;

            return _dbcontext.Set<Exam>().Where(e => e.StartDate <= today && e.EndDate >= today);

        }

        public async Task<T> GetByCretireaAsync(IFilterSpecification<T> specification)
        {
            return await _dbcontext.Set<T>().FirstOrDefaultAsync(specification.Criteria);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id).AsTask();
        }

        public Task<T> UpdateAsync(T entity)
        {
            _dbcontext.Update(entity);
            return Task.FromResult(entity);
        }
        public IQueryable<T> GetAll()
        {


            return _dbcontext.Set<T>();

        }
    }
}