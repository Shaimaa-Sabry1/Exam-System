using Exam_System.Domain.Entities;

namespace Exam_System.Shared.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity) ;
        Task DeleteAsync(T entity) ;
        Task<T?> GetByIdAsync(int id);
        Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<IQueryable<Exam>> GetAllExamAsync();
        Task<T> GetByCretireaAsync(IFilterSpecification<T> specification);
        IQueryable<T> GetAll();


    }
}
