using Exam_System.Domain.Entities;
using Exam_System.Feature.Answer.SubmitExam;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Answer.SubmitExam.Commands
{
    public record AutoSubmitExpiredExamCommand(
        int AttemptId,
        int UserId,
        Exam_System.Domain.Entities.Exam Exam
    ) : IRequest<ResponseResult<SubmitExamResponseDto>>;

    public class AutoSubmitExpiredExamCommandHandler 
        : IRequestHandler<AutoSubmitExpiredExamCommand, ResponseResult<SubmitExamResponseDto>>
    {
        private readonly GenaricRepository<attembt> _attemptRepository;
        private readonly GenaricRepository<Domain.Entities.Answer> _answerRepository;
        private readonly GenaricRepository<Exam_System.Domain.Entities.AnswerDetail> _answerDetailRepository;
        private readonly GenaricRepository<Choice> _choiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExamDbContext _dbContext;

        public AutoSubmitExpiredExamCommandHandler(
            GenaricRepository<attembt> attemptRepository,
            GenaricRepository<Domain.Entities.Answer> answerRepository,
            GenaricRepository<Exam_System.Domain.Entities.AnswerDetail> answerDetailRepository,
            GenaricRepository<Choice> choiceRepository,
            IUnitOfWork unitOfWork, 
            ExamDbContext dbContext)    
        {   
            _attemptRepository = attemptRepository;
            _answerRepository = answerRepository;
            _answerDetailRepository = answerDetailRepository;
            _choiceRepository = choiceRepository;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<ResponseResult<SubmitExamResponseDto>> Handle(
            AutoSubmitExpiredExamCommand request, 
            CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.GetByIdAsync(request.AttemptId);
            if (attempt == null)
            {
                return ResponseResult<SubmitExamResponseDto>.FailResponse("Attempt not found");
            }

            // Load exam questions with choices if not already loaded
            var exam = request.Exam;
            if (exam.Questions == null || !exam.Questions.Any())
            {
                exam = await _dbContext.Exams
                    .Include(e => e.Questions)
                    .ThenInclude(q => q.Choices)
                    .FirstOrDefaultAsync(e => e.Id == exam.Id, cancellationToken);
            }
            else if (exam.Questions.Any() && (exam.Questions.First().Choices == null || !exam.Questions.First().Choices.Any()))
            {
                // Load choices if not loaded
                exam = await _dbContext.Exams
                    .Include(e => e.Questions)
                    .ThenInclude(q => q.Choices)
                    .FirstOrDefaultAsync(e => e.Id == exam.Id, cancellationToken);
            }

            var examQuestions = (exam?.Questions != null) ? exam.Questions.ToList() : new List<Exam_System.Domain.Entities.Question>();

            // Check if answer already exists
            var existingAnswer = _answerRepository.GetAll()
                .FirstOrDefault(a => a.attembtId == attempt.Id);

            if (existingAnswer != null)
            {
                // Already has an answer, just mark as submitted
                attempt.IsSubmitted = true;
                await _attemptRepository.UpdateAsync(attempt);
                await _unitOfWork.SaveChangesAsync();

                var answerDetails = _answerDetailRepository.GetAll()
                    .Where(ad => ad.AnswerId == existingAnswer.Id)
                    .ToList();

                // Build wrong answer details
                var wrongAnswers = new List<SubmitExam.WrongAnswerDetailDto>();
                var wrongAnswerDetails = answerDetails.Where(ad => !ad.IsCorrect).ToList();

                foreach (var answerDetail in wrongAnswerDetails)
                {
                    var question = examQuestions.FirstOrDefault(q => q.Id == answerDetail.QuestionId);
                    if (question == null) continue;

                    var allQuestionChoices = question.Choices?.ToList() ?? 
                        _choiceRepository.GetAll()
                            .Where(c => c.QuestionId == answerDetail.QuestionId)
                            .ToList();

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
                        .Where(c => answerDetail.SelectedChoiceIds.Contains(c.Id))
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
                        SelectedChoiceIds = answerDetail.SelectedChoiceIds,
                        CorrectChoiceIds = correctChoiceIds,
                        SelectedChoices = selectedChoices,
                        CorrectChoices = correctChoices
                    });
                }

                var response = new SubmitExamResponseDto
                {
                    IsSuccess = true,
                    Score = existingAnswer.Score,
                    TotalQuestions = examQuestions.Count,
                    CorrectAnswers = answerDetails.Count(ad => ad.IsCorrect),
                    WrongAnswers = wrongAnswers.Count,
                    SubmittedAt = existingAnswer.SubmittedAt,
                    IsAutoSubmitted = true,
                    Message = "Exam was automatically submitted due to time expiration",
                    WrongAnswerDetails = wrongAnswers
                };

                return ResponseResult<SubmitExamResponseDto>.SuccessResponse(response);
            }

            // Create empty answer if no answer exists
            var answer = new Domain.Entities.Answer
            {
                UserId = request.UserId,
                attembtId = attempt.Id,
                SubmittedAt = DateTime.Now,
                Score = 0
            };

            await _answerRepository.AddAsync(answer);
            attempt.IsSubmitted = true;
            await _attemptRepository.UpdateAsync(attempt);
            await _unitOfWork.SaveChangesAsync();

            var emptyResponse = new SubmitExamResponseDto
            {
                IsSuccess = true,
                Score = 0,
                TotalQuestions = examQuestions.Count,
                CorrectAnswers = 0,
                WrongAnswers = 0,
                SubmittedAt = answer.SubmittedAt,
                IsAutoSubmitted = true,
                Message = "Exam was automatically submitted due to time expiration",
                WrongAnswerDetails = new List<SubmitExam.WrongAnswerDetailDto>()
            };

            return ResponseResult<SubmitExamResponseDto>.SuccessResponse(emptyResponse);
        }
    }
}

