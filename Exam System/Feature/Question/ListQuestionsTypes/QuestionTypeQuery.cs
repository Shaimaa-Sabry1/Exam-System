using Exam_System.Feature.Question.ListQuestionsTypes.Dtos;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.ListQuestionsTypes
{
    public record QuestionTypeQuery : IRequest<ResponseResult<List<QuestionTypeDto>>>
    {
    }
}
