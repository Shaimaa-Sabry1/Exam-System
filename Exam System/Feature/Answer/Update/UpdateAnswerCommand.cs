using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Answer.Update
{
    public record UpdateAnswerCommand(
        int AnswerId,
        int? Score = null,
        DateTime? SubmittedAt = null
    ) : IRequest<ResponseResult<AnswerResponseDto>>;

    public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, ResponseResult<AnswerResponseDto>>
    {
        private readonly GenaricRepository<Domain.Entities.Answer> _answerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAnswerCommandHandler(
            GenaricRepository<Domain.Entities.Answer> answerRepository,
            IUnitOfWork unitOfWork)
        {
            _answerRepository = answerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResult<AnswerResponseDto>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _answerRepository.GetByIdAsync(request.AnswerId);
            if (answer == null)
            {
                return ResponseResult<AnswerResponseDto>.FailResponse($"Answer with Id {request.AnswerId} not found");
            }

            // Update only provided fields
            if (request.Score.HasValue)
            {
                answer.Score = request.Score.Value;
            }

            if (request.SubmittedAt.HasValue)
            {
                answer.SubmittedAt = request.SubmittedAt.Value;
            }

            await _answerRepository.UpdateAsync(answer);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == 0)
            {
                return ResponseResult<AnswerResponseDto>.FailResponse("Failed to update answer");
            }

            var response = new AnswerResponseDto
            {
                Id = answer.Id,
                UserId = answer.UserId,
                AttemptId = answer.attembtId,
                Score = answer.Score,
                SubmittedAt = answer.SubmittedAt
            };

            return ResponseResult<AnswerResponseDto>.SuccessResponse(response, "Answer updated successfully");
        }
    }

    public class AnswerResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AttemptId { get; set; }
        public int Score { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}

