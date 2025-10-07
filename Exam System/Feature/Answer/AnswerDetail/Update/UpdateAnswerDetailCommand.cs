using Exam_System.Domain.Entities;
using Exam_System.Feature.Answer.DTO;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Answer.AnswerDetail.Update
{
    public record UpdateAnswerDetailCommand(
        int AnswerDetailId,
        int? QuestionId = null,
        List<int>? SelectedChoiceIds = null
    ) : IRequest<ResponseResult<AnswerDetailResponseDto>>;

    public class UpdateAnswerDetailCommandHandler : IRequestHandler<UpdateAnswerDetailCommand, ResponseResult<AnswerDetailResponseDto>>
    {
        private readonly GenaricRepository<Domain.Entities.AnswerDetail> _answerDetailRepository;
        private readonly GenaricRepository<Choice> _choiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAnswerDetailCommandHandler(
            GenaricRepository<Domain.Entities.AnswerDetail> answerDetailRepository,
            GenaricRepository<Choice> choiceRepository,
            IUnitOfWork unitOfWork)
        {
            _answerDetailRepository = answerDetailRepository;
            _choiceRepository = choiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResult<AnswerDetailResponseDto>> Handle(UpdateAnswerDetailCommand request, CancellationToken cancellationToken)
        {
            var answerDetail = await _answerDetailRepository.GetByIdAsync(request.AnswerDetailId);
            if (answerDetail == null)
            {
                return ResponseResult<AnswerDetailResponseDto>.FailResponse($"AnswerDetail with Id {request.AnswerDetailId} not found");
            }

            // Update only provided fields
            if (request.QuestionId.HasValue)
            {
                answerDetail.QuestionId = request.QuestionId.Value;
            }

            if (request.SelectedChoiceIds != null)
            {
                answerDetail.SelectedChoiceIds = request.SelectedChoiceIds;
                
                // Recalculate IsCorrect based on new selections
                var allQuestionChoices = _choiceRepository.GetAll()
                    .Where(c => c.QuestionId == answerDetail.QuestionId)
                    .ToList();

                answerDetail.IsCorrect = ValidateAnswer(answerDetail.SelectedChoiceIds, allQuestionChoices);
            }

            await _answerDetailRepository.UpdateAsync(answerDetail);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == 0)
            {
                return ResponseResult<AnswerDetailResponseDto>.FailResponse("Failed to update answer detail");
            }

            var response = new AnswerDetailResponseDto
            {
                IsSuccess = true
            };

            return ResponseResult<AnswerDetailResponseDto>.SuccessResponse(response, "AnswerDetail updated successfully");
        }

        private bool ValidateAnswer(List<int> selectedChoiceIds, List<Choice> allQuestionChoices)
        {
            if (selectedChoiceIds == null || selectedChoiceIds.Count == 0)
            {
                return false;
            }

            var correctChoices = allQuestionChoices.Where(c => c.IsCorrect).Select(c => c.Id).ToList();
            var incorrectChoices = allQuestionChoices.Where(c => !c.IsCorrect).Select(c => c.Id).ToList();

            // Check if all selected choices are correct AND all correct choices are selected
            bool allSelectedAreCorrect = selectedChoiceIds.All(id => correctChoices.Contains(id));
            bool allCorrectAreSelected = correctChoices.All(id => selectedChoiceIds.Contains(id));
            bool noIncorrectSelected = !selectedChoiceIds.Any(id => incorrectChoices.Contains(id));

            return allSelectedAreCorrect && allCorrectAreSelected && noIncorrectSelected;
        }
    }
}

