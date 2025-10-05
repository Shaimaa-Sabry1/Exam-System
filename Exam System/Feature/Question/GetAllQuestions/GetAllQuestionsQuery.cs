using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.GetAllQuestions
{
    public record GetAllQuestionsQuery(int ExamId):IRequest<ResponseResult<GetAllQuestionsResponse>>;
   
}
