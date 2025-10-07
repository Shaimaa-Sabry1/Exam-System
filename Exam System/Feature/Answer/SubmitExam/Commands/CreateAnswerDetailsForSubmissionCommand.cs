using Exam_System.Domain.Entities;
using Exam_System.Feature.Answer.SubmitExam;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Answer.SubmitExam.Commands
{
    public record CreateAnswerDetailsForSubmissionCommand(
        int AnswerId,
        List<AnswerDetailSubmissionDto> AnswerDetails,
        List<Exam_System.Domain.Entities.Question> ExamQuestions
    ) : IRequest<CreateAnswerDetailsResponseDto>;

    public class CreateAnswerDetailsForSubmissionCommandHandler 
        : IRequestHandler<CreateAnswerDetailsForSubmissionCommand, CreateAnswerDetailsResponseDto>
    {
        private readonly GenaricRepository<Exam_System.Domain.Entities.AnswerDetail> _answerDetailRepository;
        private readonly GenaricRepository<Choice> _choiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAnswerDetailsForSubmissionCommandHandler(
            GenaricRepository<Exam_System.Domain.Entities.AnswerDetail> answerDetailRepository,
            GenaricRepository<Choice> choiceRepository,
            IUnitOfWork unitOfWork)
        {
            _answerDetailRepository = answerDetailRepository;
            _choiceRepository = choiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateAnswerDetailsResponseDto> Handle(
            CreateAnswerDetailsForSubmissionCommand request, 
            CancellationToken cancellationToken)
        {
            // Get all choices for validation
            var allSelectedChoiceIds = request.AnswerDetails
                .SelectMany(ad => ad.SelectedChoiceIds)
                .Distinct()
                .ToList();

            var choices = _choiceRepository.GetAll()
                .Where(c => allSelectedChoiceIds.Contains(c.Id))
                .ToList();

            int totalScore = 0;
            int correctAnswers = 0;
            var wrongAnswers = new List<SubmitExam.WrongAnswerDetailDto>();

            // Create AnswerDetails and calculate score
            foreach (var answerDetailDto in request.AnswerDetails)
            {
                var question = request.ExamQuestions.FirstOrDefault(q => q.Id == answerDetailDto.QuestionId);
                if (question == null)
                {
                    continue; // Skip invalid question IDs
                }

                // Get all choices for this question
                var allQuestionChoices = _choiceRepository.GetAll()
                    .Where(c => c.QuestionId == answerDetailDto.QuestionId)
                    .ToList();

                // Validate answer correctness
                var isCorrect = ValidateAnswer(answerDetailDto.SelectedChoiceIds, allQuestionChoices);

                var answerDetail = new Exam_System.Domain.Entities.AnswerDetail
                {
                    AnswerId = request.AnswerId,
                    QuestionId = answerDetailDto.QuestionId,
                    SelectedChoiceIds = answerDetailDto.SelectedChoiceIds,
                    IsCorrect = isCorrect
                };

                await _answerDetailRepository.AddAsync(answerDetail);

                if (isCorrect)
                {
                    correctAnswers++;
                    totalScore++; // Each correct answer = 1 point
                }
                else
                {
                    // Collect wrong answer details
                    var correctChoiceIds = allQuestionChoices
                        .Where(c => c.IsCorrect)
                        .Select(c => c.Id)
                        .ToList();

                    var correctChoices = allQuestionChoices
                        .Where(c => c.IsCorrect)
                        .Select(c => new SubmitExam.ChoiceDetailDto
                        {
                            Id = c.Id,
                            Text = c.Text,
                            IsCorrect = true
                        })
                        .ToList();

                    var selectedChoices = allQuestionChoices
                        .Where(c => answerDetailDto.SelectedChoiceIds.Contains(c.Id))
                        .Select(c => new SubmitExam.ChoiceDetailDto
                        {
                            Id = c.Id,
                            Text = c.Text,
                            IsCorrect = c.IsCorrect
                        })
                        .ToList();

                    wrongAnswers.Add(new SubmitExam.WrongAnswerDetailDto
                    {
                        QuestionId = question.Id,
                        QuestionTitle = question.Title,
                        SelectedChoiceIds = answerDetailDto.SelectedChoiceIds,
                        CorrectChoiceIds = correctChoiceIds,
                        SelectedChoices = selectedChoices,
                        CorrectChoices = correctChoices
                    });
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return new CreateAnswerDetailsResponseDto
            {
                TotalScore = totalScore,
                CorrectAnswers = correctAnswers,
                WrongAnswers = wrongAnswers
            };
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

    public class CreateAnswerDetailsResponseDto
    {
        public int TotalScore { get; set; }
        public int CorrectAnswers { get; set; }
        public List<SubmitExam.WrongAnswerDetailDto> WrongAnswers { get; set; } = new List<SubmitExam.WrongAnswerDetailDto>();
    }
}

