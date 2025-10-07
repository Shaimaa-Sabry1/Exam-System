using Exam_System.Domain.Exception;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Answer.AnswerDetail.Delete
{
    public record DeleteAnswerDetailCommand(int AnswerDetailId) : IRequest<ResponseResult<bool>>;

    public class DeleteAnswerDetailCommandHandler : IRequestHandler<DeleteAnswerDetailCommand, ResponseResult<bool>>
    {
        private readonly GenaricRepository<Domain.Entities.AnswerDetail> _answerDetailRepository;
        private readonly GenaricRepository<Domain.Entities.Answer> _answerRepository;
        private readonly GenaricRepository<Exam_System.Domain.Entities. attembt> _attemptRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExamDbContext _dbContext;

        public DeleteAnswerDetailCommandHandler(
            GenaricRepository<Domain.Entities.AnswerDetail> answerDetailRepository,
            GenaricRepository<Domain.Entities.Answer> answerRepository,
            GenaricRepository<Exam_System.Domain.Entities.attembt> attemptRepository,
            IUnitOfWork unitOfWork,
            ExamDbContext dbContext)
        {
            _answerDetailRepository = answerDetailRepository;
            _answerRepository = answerRepository;
            _attemptRepository = attemptRepository;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<ResponseResult<bool>> Handle(DeleteAnswerDetailCommand request, CancellationToken cancellationToken)
        {
            var answerDetail = await _answerDetailRepository.GetByIdAsync(request.AnswerDetailId);
            if (answerDetail == null)
            {
                return ResponseResult<bool>.FailResponse($"AnswerDetail with Id {request.AnswerDetailId} not found");
            }

            // Get the answer to find user and attempt
            var answer = await _answerRepository.GetByIdAsync(answerDetail.AnswerId);
            if (answer == null)
            {
                return ResponseResult<bool>.FailResponse("Answer not found");
            }

            // Get the attempt to find exam ID
            var attempt = await _attemptRepository.GetByIdAsync(answer.attembtId);
            if (attempt == null)
            {
                return ResponseResult<bool>.FailResponse("Attempt not found");
            }

            // If this is a wrong answer, check if user has answered this question correctly in newer attempts
            if (!answerDetail.IsCorrect)
            {
                // Find all newer answers for the same user and exam
                var newerAnswers = await _dbContext.Answers
                    .Include(a => a.Attembt)
                    .Where(a => a.UserId == answer.UserId 
                        && a.Attembt.ExamId == attempt.ExamId 
                        && a.SubmittedAt > answer.SubmittedAt)
                    .OrderByDescending(a => a.SubmittedAt)
                    .ToListAsync(cancellationToken);

                // Check if this question was answered correctly in any newer attempt
                foreach (var newerAnswer in newerAnswers)
                {
                    var newerAnswerDetails = await _dbContext.Set<Domain.Entities.AnswerDetail>()
                        .Where(ad => ad.AnswerId == newerAnswer.Id 
                            && ad.QuestionId == answerDetail.QuestionId 
                            && ad.IsCorrect)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (newerAnswerDetails != null)
                    {
                        // Question is now answered correctly in a newer attempt, safe to delete
                        break;
                    }

                    // Check if question is still wrong in this newer attempt
                    var newerWrongDetail = await _dbContext.Set<Domain.Entities.AnswerDetail>()
                        .Where(ad => ad.AnswerId == newerAnswer.Id 
                            && ad.QuestionId == answerDetail.QuestionId 
                            && !ad.IsCorrect)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (newerWrongDetail != null)
                    {
                        // Question is still wrong in newer attempt, preserve the old wrong answer
                        return ResponseResult<bool>.FailResponse(
                            "Cannot delete wrong answer detail. The question is still answered incorrectly in newer attempts. " +
                            "Wrong answer details are preserved to track learning progress.");
                    }
                }
            }

            // Safe to delete: either it's a correct answer, or question is now correct in newer attempts
            await _answerDetailRepository.DeleteAsync(answerDetail);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == 0)
            {
                return ResponseResult<bool>.FailResponse("Failed to delete answer detail");
            }

            return ResponseResult<bool>.SuccessResponse(true, "AnswerDetail deleted successfully");
        }
    }
}

