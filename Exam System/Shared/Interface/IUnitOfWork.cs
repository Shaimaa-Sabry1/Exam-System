namespace Exam_System.Shared.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IExamRepository Exam { get; }
        IQuestionRepository Question { get; }
        Task<int> SaveChangesAsync();
    }
}
