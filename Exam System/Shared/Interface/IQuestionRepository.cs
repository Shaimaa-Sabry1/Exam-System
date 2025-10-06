using Exam_System.Domain.Entities;

namespace Exam_System.Shared.Interface
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        //Task<(IEnumerable<Question>, int)> GetAllAsync(int ExamId);
        //Task<(IEnumerable<Question>, int)> GetAllAsync(string QuestionName);
        Task<(IEnumerable<Question>, int)> GetAllQuestionsAsync(int ExamId);
        Task<(IEnumerable<Question>, int)> GetAllQuestionsAsync(string QuestionName);

    }
}