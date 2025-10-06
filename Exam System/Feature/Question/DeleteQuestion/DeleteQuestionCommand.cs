using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.DeleteQuestion
{
    public record DeleteQuestionCommand(int QuestionId):IRequest<ResponseResult<bool>>;
  
}
