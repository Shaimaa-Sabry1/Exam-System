namespace Exam_System.Shared.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IExamRepository Exam { get; }

        Task<int> SaveChangesAsync();
    }
}
