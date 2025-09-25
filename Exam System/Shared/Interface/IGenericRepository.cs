namespace Exam_System.Shared.Interface
{
    public interface IGenericRepository<T>
    {
        Task<T> AddAsync<T>(T entity);
        Task<T> UpdateAsync<T>(T entity) ;
        Task DeleteAsync<T>(T entity) ;
        Task<T?> GetByIdAsync<T>(Guid id);
        Task<IEnumerable<T>> GetAllAsync<T>();

    }
}
