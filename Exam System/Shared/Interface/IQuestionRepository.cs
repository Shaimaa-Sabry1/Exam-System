using Exam_System.Domain.Entities;

namespace Exam_System.Shared.Interface
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<Question> GetQuestionsByIdWithChoicesAsync(int QuestionId);
        Task<(IEnumerable<Question>, int)> GetAllQuestionsAsync(IFilterSpecification<Question> criteria);
    }
}