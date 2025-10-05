using Exam_System.Domain.Exception;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.DeleteQuestion
{
    public class DeleteQuestionCommandHandler(ExamDbContext _dbContext,IQuestionRepository _questionRepository) : IRequestHandler<DeleteQuestionCommand, ResponseResult<bool>>
    {
        public async Task<ResponseResult<bool>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            if (question == null)
                return ResponseResult<bool>.FailResponse($"Question With Id={request.QuestionId} Not Found.");
            await _questionRepository.DeleteAsync(question);
            await _dbContext.SaveChangesAsync();
            return ResponseResult<bool>.SuccessResponse(true , $"Question deleted successfully.");
        }
    }
}
