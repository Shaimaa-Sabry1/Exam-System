namespace Exam_System.Shared.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        
        Task<int> SaveChangesAsync();
    }
}
