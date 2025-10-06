using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.GetAllQuestions
{
    public record GetAllQuestionsQuery(int? ExamId = null, string? QuestionName = null) :IRequest<ResponseResult<GetAllQuestionsResponse>>;
   
}
