using Exam_System.Feature.Question.AddQuestion.Dtos;
using Exam_System.Feature.Question.EditQuestion.Dtos;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Questions.EditQuestion
{
    public record EditQuestionCommand(int QuestionId, string Title, string Type, List<ChoiceDto> Choices) : IRequest<ResponseResult<EditQuestionToReturnDto>>
    {
    }
}
