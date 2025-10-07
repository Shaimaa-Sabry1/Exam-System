using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Helpers;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Answer.SubmitExam.Commands
{
    public record ValidateExamSubmissionCommand(int AttemptId) : IRequest<ResponseResult<ValidateExamSubmissionResponseDto>>;

    public class ValidateExamSubmissionCommandHandler : IRequestHandler<ValidateExamSubmissionCommand, ResponseResult<ValidateExamSubmissionResponseDto>>
    {
        private readonly GenaricRepository<attembt> _attemptRepository;
        private readonly ExamDbContext _dbContext;

        public ValidateExamSubmissionCommandHandler(
            GenaricRepository<attembt> attemptRepository,
            ExamDbContext dbContext)
        {
            _attemptRepository = attemptRepository;
            _dbContext = dbContext;
        }

        public async Task<ResponseResult<ValidateExamSubmissionResponseDto>> Handle(ValidateExamSubmissionCommand request, CancellationToken cancellationToken)
        {
            // Get the attempt
            var attempt = await _attemptRepository.GetByIdAsync(request.AttemptId);
            if (attempt == null)
            {
                return ResponseResult<ValidateExamSubmissionResponseDto>.FailResponse("Attempt not found");
            }

            // Check if already submitted
            if (attempt.IsSubmitted == true)
            {
                return ResponseResult<ValidateExamSubmissionResponseDto>.FailResponse("Exam has already been submitted");
            }

            // Get exam details with questions loaded
            var exam = await _dbContext.Exams
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(e => e.Id == attempt.ExamId, cancellationToken);

            if (exam == null)
            {
                return ResponseResult<ValidateExamSubmissionResponseDto>.FailResponse("Exam not found");
            }

            // Validate exam duration
            var isTimeExpired = ExamDurationHelper.IsExamTimeExpired(attempt, exam);
            var isActive = ExamDurationHelper.IsExamActive(exam);
            var remainingTime = ExamDurationHelper.GetRemainingTime(attempt, exam);

            var response = new ValidateExamSubmissionResponseDto
            {
                IsValid = !isTimeExpired && isActive,
                IsTimeExpired = isTimeExpired,
                IsActive = isActive,
                Attempt = attempt,
                Exam = exam,
                RemainingTime = remainingTime
            };

            if (!response.IsValid)
            {
                var errorMessage = isTimeExpired 
                    ? "Exam time has expired" 
                    : "Exam is not currently active";
                return ResponseResult<ValidateExamSubmissionResponseDto>.FailResponse(errorMessage);
            }

            return ResponseResult<ValidateExamSubmissionResponseDto>.SuccessResponse(response);
        }
    }

    public class ValidateExamSubmissionResponseDto
    {
        public bool IsValid { get; set; }
        public bool IsTimeExpired { get; set; }
        public bool IsActive { get; set; }
        public attembt Attempt { get; set; }
        public Exam_System.Domain.Entities.Exam Exam { get; set; }
        public TimeSpan RemainingTime { get; set; }
    }
}

