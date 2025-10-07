using Exam_System.Domain.Entities;
using Exam_System.Feature.Answer.Delete;
using Exam_System.Feature.Answer.SubmitExam.Commands;
using Exam_System.Feature.Answer.SubmitExam;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Answer.SubmitExam.Orchestrator.SubmitExam
{
    public record SubmitExamCommand(
        int AttemptId,
        int UserId,
        List<AnswerDetailSubmissionDto> AnswerDetails
    ) : IRequest<ResponseResult<SubmitExamResponseDto>>;

    public class SubmitExamCommandHandler : IRequestHandler<SubmitExamCommand, ResponseResult<SubmitExamResponseDto>>
    {
        private readonly IMediator _mediator;

        public SubmitExamCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseResult<SubmitExamResponseDto>> Handle(
            SubmitExamCommand request, 
            CancellationToken cancellationToken)
        {
            // Step 1: Validate exam submission
            var validationResult = await _mediator.Send(
                new ValidateExamSubmissionCommand(request.AttemptId), 
                cancellationToken);

            if (!validationResult.IsSuccess)
            {
                // Check if it's a time expiration - handle auto-submit
                if (validationResult.Data?.IsTimeExpired == true)
                {
                    var autoSubmitResult = await _mediator.Send(
                        new AutoSubmitExpiredExamCommand(
                            request.AttemptId,
                            request.UserId,
                            validationResult.Data.Exam),
                        cancellationToken);
                    return autoSubmitResult;
                }

                // Return validation error
                return ResponseResult<SubmitExamResponseDto>.FailResponse(validationResult.Message);
            }

            var validationData = validationResult.Data;
            if (validationData == null)
            {
                return ResponseResult<SubmitExamResponseDto>.FailResponse("Validation failed");
            }

            // Step 2: Create Answer record
            var answerId = await _mediator.Send(
                new CreateAnswerForSubmissionCommand(request.UserId, request.AttemptId),
                cancellationToken);

            // Step 3: Create AnswerDetails and calculate score
            var answerDetailsResult = await _mediator.Send(
                new CreateAnswerDetailsForSubmissionCommand(
                    answerId,
                    request.AnswerDetails,
                    validationData.Exam.Questions?.ToList() ?? new List<Exam_System.Domain.Entities.Question>()),
                cancellationToken); 

            // Step 4: Update Answer score
            await _mediator.Send(
                new UpdateAnswerScoreCommand(answerId, answerDetailsResult.TotalScore),
                cancellationToken);

            // Step 5: Mark attempt as submitted
            await _mediator.Send(
                new MarkAttemptAsSubmittedCommand(request.AttemptId),
                cancellationToken);

            // Step 6: Delete old answers with lower scores (if any)
            // This automatically cleans up old attempts when user gets a better score
            var deleteResult = await _mediator.Send(
                new DeleteLowerScoreAnswerCommand(answerId),
                cancellationToken);
            // Note: We don't fail if deletion fails, it's just cleanup

            // Step 7: Get the answer to get submission time
            var answer = await _mediator.Send(
                new GetAnswerByIdQuery(answerId),
                cancellationToken);

            // Build response
            var response = new SubmitExamResponseDto
            {
                IsSuccess = true,
                Score = answerDetailsResult.TotalScore,
                TotalQuestions = validationData.Exam.Questions?.Count ?? 0,
                CorrectAnswers = answerDetailsResult.CorrectAnswers,
                WrongAnswers = answerDetailsResult.WrongAnswers?.Count ?? 0,
                SubmittedAt = answer?.SubmittedAt ?? DateTime.Now,
                WrongAnswerDetails = answerDetailsResult.WrongAnswers ?? new List<WrongAnswerDetailDto>()
            };

            return ResponseResult<SubmitExamResponseDto>.SuccessResponse(response);
        }
    }

    // Query to get answer by ID
    public record GetAnswerByIdQuery(int AnswerId) : IRequest<Exam_System.Domain.Entities.Answer?>;

    public class GetAnswerByIdQueryHandler : IRequestHandler<GetAnswerByIdQuery, Exam_System.Domain.Entities.Answer?>
    {   
        private readonly GenaricRepository<Exam_System.Domain.Entities.Answer> _answerRepository;

        public GetAnswerByIdQueryHandler(GenaricRepository<Exam_System.Domain.Entities.Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Exam_System.Domain.Entities.Answer?> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _answerRepository.GetByIdAsync(request.AnswerId);
        }
    }
}

