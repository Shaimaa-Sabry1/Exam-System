using Exam_System.Shared.Interface;

namespace Exam_System.Infrastructure.Repositories
{
    public class GenaricRepository : IGenericRepository
    {
        public Task<T> AddAsync<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
