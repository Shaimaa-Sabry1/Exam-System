using Exam_System.Domain.Entities;
using Exam_System.Feature.Answer.DTO;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Exam_System.Feature.Answer.AnswerDetail
{
    // Refactored to accept separated properties
    public record CreateAnswerDetailsCommand(
        int AnswerId,
        int QuestionId,
        List<int> SelectedChoiceIds
    ) : IRequest<ResponseResult<AnswerDetailResponseDto>>;

    public class CreateAnswerDetailsCommandHandler : IRequestHandler<CreateAnswerDetailsCommand, ResponseResult<AnswerDetailResponseDto>>
    {
        private readonly GenaricRepository<Exam_System.Domain.Entities.AnswerDetail> _ADrepository;
        private readonly GenaricRepository<Choice> _choiceRebo;
        private readonly IDistributedCache _Cache;
        private readonly IUnitOfWork unitOfWork;

                public CreateAnswerDetailsCommandHandler(
            GenaricRepository<Exam_System.Domain.Entities.AnswerDetail> ADrepository,
            GenaricRepository<Choice> ChoiceRebo,
            IDistributedCache cache,
            IUnitOfWork unitOfWork)
            => (_ADrepository, _choiceRebo, _Cache, this.unitOfWork) = (ADrepository, ChoiceRebo, cache, unitOfWork);

        public async Task<ResponseResult<AnswerDetailResponseDto>> Handle(CreateAnswerDetailsCommand request, CancellationToken cancellationToken)
        {
            // Get all choices for the question to validate correctness
            var allQuestionChoices = _choiceRebo.GetAll()
                .Where(c => c.QuestionId == request.QuestionId)
                .ToList();

            if (!allQuestionChoices.Any())
            {
                return ResponseResult<AnswerDetailResponseDto>.FailResponse("Question not found or has no choices");
            }

            // Validate answer correctness
            var correctChoices = allQuestionChoices.Where(c => c.IsCorrect).Select(c => c.Id).ToList();
            var incorrectChoices = allQuestionChoices.Where(c => !c.IsCorrect).Select(c => c.Id).ToList();

            // Check if all selected choices are correct AND all correct choices are selected
            bool allSelectedAreCorrect = request.SelectedChoiceIds.All(id => correctChoices.Contains(id));
            bool allCorrectAreSelected = correctChoices.All(id => request.SelectedChoiceIds.Contains(id));
            bool noIncorrectSelected = !request.SelectedChoiceIds.Any(id => incorrectChoices.Contains(id));

            var isCorrect = allSelectedAreCorrect && allCorrectAreSelected && noIncorrectSelected;

            // Create the answer detail
            var answerDetail = new Exam_System.Domain.Entities.AnswerDetail
            {
                AnswerId = request.AnswerId,
                QuestionId = request.QuestionId,
                SelectedChoiceIds = request.SelectedChoiceIds ?? new List<int>(),
                IsCorrect = isCorrect
            };

            await _ADrepository.AddAsync(answerDetail);
            var isCompleted = await unitOfWork.SaveChangesAsync();

            if (isCompleted == 0)
            {
                return ResponseResult<AnswerDetailResponseDto>.FailResponse("Answer Detail not created");
            }

            var response = new AnswerDetailResponseDto() { IsSuccess = true };
            return ResponseResult<AnswerDetailResponseDto>.SuccessResponse(response);
        }
    }


}
