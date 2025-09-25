using Exam_System.Shared.Interface;

namespace Exam_System.Infrastructure.Repositories
{
    public class GenaricRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<T1> AddAsync<T1>(T1 entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T1>(T1 entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T1>> GetAllAsync<T1>()
        {
            throw new NotImplementedException();
        }

        public Task<T1> GetByIdAsync<T1>(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T1> UpdateAsync<T1>(T1 entity)
        {
            throw new NotImplementedException();
        }
    }
}
