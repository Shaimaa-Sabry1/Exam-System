using Exam_System.Domain.Entities;

namespace Exam_System.Shared.Interface
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<(IEnumerable<Question>, int)> GetAllAsync(int ExamId);
    }
}