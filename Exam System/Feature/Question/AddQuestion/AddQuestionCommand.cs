using Exam_System.Domain.Entities;
using Exam_System.Feature.Question.AddQuestion;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Questions.AddQuestions
{
    public record AddQuestionCommand(string Title, string Type, int ExamId, List<ChoiceDto> Choices) : IRequest<ResponseResult<AddQuestionToReturnDto>>
    {
    }
}
