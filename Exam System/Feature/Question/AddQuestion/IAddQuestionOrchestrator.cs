using Exam_System.Domain.Entities;
using Exam_System.Feature.Question.AddQuestion;
using Exam_System.Shared.Response;

namespace Exam_System.Feature.Questions.AddQuestions
{
    public interface IAddQuestionOrchestrator
    {
        Task<ResponseResult<AddQuestionToReturnDto>> AddAsync(string Title, string Type, int ExamId, List<ChoiceDto> Choices);
    }
}