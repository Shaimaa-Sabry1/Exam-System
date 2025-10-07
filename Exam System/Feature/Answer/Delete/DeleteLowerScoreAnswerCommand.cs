using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Answer.Delete
{
    /// <summary>
    /// Deletes old answers with lower scores when a new answer with higher score exists for the same exam.
    /// Preserves wrong answer details if the question is still answered incorrectly in the new attempt.
    /// </summary>
    public record DeleteLowerScoreAnswerCommand(int NewAnswerId) : IRequest<ResponseResult<DeleteLowerScoreAnswerResponseDto>>;

    public class DeleteLowerScoreAnswerCommandHandler : IRequestHandler<DeleteLowerScoreAnswerCommand, ResponseResult<DeleteLowerScoreAnswerResponseDto>>
    {
        private readonly GenaricRepository<Domain.Entities.Answer> _answerRepository;
        private readonly GenaricRepository<attembt> _attemptRepository;
        private readonly GenaricRepository<Domain.Entities.AnswerDetail> _answerDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExamDbContext _dbContext;

        public DeleteLowerScoreAnswerCommandHandler(
            GenaricRepository<Domain.Entities.Answer> answerRepository,
            GenaricRepository<attembt> attemptRepository,
            GenaricRepository<Domain.Entities.AnswerDetail> answerDetailRepository,
            IUnitOfWork unitOfWork,
            ExamDbContext dbContext)
        {
            _answerRepository = answerRepository;
            _attemptRepository = attemptRepository;
            _answerDetailRepository = answerDetailRepository;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<ResponseResult<DeleteLowerScoreAnswerResponseDto>> Handle(
            DeleteLowerScoreAnswerCommand request, 
            CancellationToken cancellationToken)
        {
            // Get the new answer
            var newAnswer = await _answerRepository.GetByIdAsync(request.NewAnswerId);
            if (newAnswer == null)
            {
                return ResponseResult<DeleteLowerScoreAnswerResponseDto>.FailResponse($"Answer with Id {request.NewAnswerId} not found");
            }

            // Get the attempt to find exam ID
            var newAttempt = await _attemptRepository.GetByIdAsync(newAnswer.attembtId);
            if (newAttempt == null)
            {
                return ResponseResult<DeleteLowerScoreAnswerResponseDto>.FailResponse("Attempt not found");
            }

            // Find all other answers for the same user and exam with lower scores
            var oldAnswers = await _dbContext.Answers
                .Include(a => a.Attembt)
                .Where(a => a.UserId == newAnswer.UserId 
                    && a.Attembt.ExamId == newAttempt.ExamId 
                    && a.Id != newAnswer.Id 
                    && a.Score < newAnswer.Score)
                .ToListAsync(cancellationToken);

            if (!oldAnswers.Any())
            {
                return ResponseResult<DeleteLowerScoreAnswerResponseDto>.SuccessResponse(
                    new DeleteLowerScoreAnswerResponseDto
                    {
                        DeletedAnswersCount = 0,
                        DeletedAnswerDetailsCount = 0,
                        PreservedAnswerDetailsCount = 0
                    },
                    "No lower score answers found to delete");
            }

            // Get all answer details for the new answer to check which questions were answered correctly/wrongly
            var newAnswerDetails = await _dbContext.Set<Domain.Entities.AnswerDetail>()
                .Where(ad => ad.AnswerId == newAnswer.Id)
                .ToListAsync(cancellationToken);

            var correctlyAnsweredQuestionIds = newAnswerDetails
                .Where(ad => ad.IsCorrect)
                .Select(ad => ad.QuestionId)
                .ToHashSet();

            var wronglyAnsweredQuestionIds = newAnswerDetails
                .Where(ad => !ad.IsCorrect)
                .Select(ad => ad.QuestionId)
                .ToHashSet();

            int deletedAnswerDetailsCount = 0;
            int preservedAnswerDetailsCount = 0;
            int deletedAnswersCount = 0;

            // Process each old answer
            foreach (var oldAnswer in oldAnswers)
            {
                // Get old answer details
                var oldAnswerDetails = await _dbContext.Set<Domain.Entities.AnswerDetail>()
                    .Where(ad => ad.AnswerId == oldAnswer.Id)
                    .ToListAsync(cancellationToken);

                bool hasPreservedDetails = false;

                // Process each old answer detail
                foreach (var oldDetail in oldAnswerDetails)
                {
                    // If it's a correct answer detail, always safe to delete
                    if (oldDetail.IsCorrect)
                    {
                        await _answerDetailRepository.DeleteAsync(oldDetail);
                        deletedAnswerDetailsCount++;
                    }
                    // If it's a wrong answer detail
                    else
                    {
                        // If the question is now answered correctly in the new attempt, safe to delete old wrong answer
                        if (correctlyAnsweredQuestionIds.Contains(oldDetail.QuestionId))
                        {
                            await _answerDetailRepository.DeleteAsync(oldDetail);
                            deletedAnswerDetailsCount++;
                        }
                        // If the question is still wrong in new attempt, preserve the old wrong answer detail
                        else if (wronglyAnsweredQuestionIds.Contains(oldDetail.QuestionId))
                        {
                            // Question is still wrong in new attempt, preserve the old wrong answer
                            hasPreservedDetails = true;
                            preservedAnswerDetailsCount++;
                        }
                        // Question not answered in new attempt (edge case)
                        else
                        {
                            // Question not in new attempt, preserve old wrong answer for tracking
                            hasPreservedDetails = true;
                            preservedAnswerDetailsCount++;
                        }
                    }
                }

                // Delete the old answer only if no details are preserved
                // If details are preserved, we keep the answer record (even if empty, it's okay for tracking)
                if (!hasPreservedDetails)
                {
                    await _answerRepository.DeleteAsync(oldAnswer);
                    deletedAnswersCount++;
                }
            }

            var result = await _unitOfWork.SaveChangesAsync();

            return ResponseResult<DeleteLowerScoreAnswerResponseDto>.SuccessResponse(
                new DeleteLowerScoreAnswerResponseDto
                {
                    DeletedAnswersCount = deletedAnswersCount,
                    DeletedAnswerDetailsCount = deletedAnswerDetailsCount,
                    PreservedAnswerDetailsCount = preservedAnswerDetailsCount
                },
                $"Successfully processed {oldAnswers.Count} lower score answer(s). " +
                $"Deleted {deletedAnswersCount} answer(s) and {deletedAnswerDetailsCount} answer detail(s). " +
                $"Preserved {preservedAnswerDetailsCount} wrong answer detail(s) for learning tracking.");
        }
    }

    public class DeleteLowerScoreAnswerResponseDto
    {
        public int DeletedAnswersCount { get; set; }
        public int DeletedAnswerDetailsCount { get; set; }
        public int PreservedAnswerDetailsCount { get; set; }
    }
}
