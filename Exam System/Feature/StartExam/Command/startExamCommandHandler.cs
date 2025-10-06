using Exam_System.Domain.Entities;
using Exam_System.Feature.StartExam.DTO;
using Exam_System.Infrastructure.Persistance;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Helpers;
using Exam_System.Shared.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.StartExam.Command
{
    public class startExamCommandHandler : IRequestHandler<StartExamCommand, AttemptDto>
    {
        
        private readonly GenaricRepository<Domain.Entities.StartExam> _startexamrepo;
        private readonly IShuffleService _shuffleService;
        private readonly IUnitOfWork _unitOfWork;

        public startExamCommandHandler(GenaricRepository<Exam_System.Domain.Entities.StartExam> startexamrepo, IShuffleService shuffleService, IUnitOfWork unitOfWork)
        {
            this._startexamrepo = startexamrepo;
            this._shuffleService = shuffleService;
            this._unitOfWork = unitOfWork;
        }
        public async Task<AttemptDto> Handle(StartExamCommand request, CancellationToken cancellationToken)
        {
            var today = DateTime.Today;
            var exam = await _unitOfWork.Exam.GetAll()
                .Where(e => e.StartDate <= today && e.EndDate >= today&&e.Id==request.ExamId)
                .Include(e => e.Questions)
                .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(e => e.Id == request.ExamId, cancellationToken);

            if (exam == null)
                throw new Exception("Exam not found");

            var existingAttempt = _startexamrepo.GetAll()
                .FirstOrDefault(a => a.examId == request.ExamId && a.userId == request.UserId && a.endTime == null);

            if (existingAttempt != null)
                throw new Exception("You already have an active attempt for this exam.");

            var shuffledQuestions = _shuffleService.shuffle(exam.Questions.ToList());
            var attempt = new Exam_System.Domain.Entities.StartExam
            {
                examId = exam.Id,
                userId = request.UserId,
                startTime = DateTime.Now,
                Exam = exam,
                DurationTakenMinutes = exam.DurationInMinutes,
                score = 0

            };

            // Save the attempt first to generate attemptId
         

            int order = 1;
            foreach (var question in shuffledQuestions)
            {
                var shuffledChoices = _shuffleService.shuffle(question.Choices.ToList());
                var choiceOrder = shuffledChoices.Select(c => c.Id).ToList();

                var attemptQuestion = new AttemptQuestion
                {
                    startExamId = attempt.attemptId, // Set FK explicitly
                    questionId = question.Id,
                    order = order++,
                    ChoiceOrder = choiceOrder
                };

                attempt.AttemptQuestions.Add(attemptQuestion);
            }
            await _startexamrepo.AddAsync(attempt);
            await _unitOfWork.SaveChangesAsync();


            
            var dto = new AttemptDto
            {
                AttemptId = attempt.attemptId,
                ExamTitle = exam.Title,
                DurationInMinutes= exam.DurationInMinutes,
                StartedAt = attempt.startTime,
                Questions = attempt.AttemptQuestions
                      .OrderBy(aq => aq.order)
                      .Select(aq => new AttemptQuestionDto
                      {
                          AttemptQuestionId = aq.attemptQuestionId,
                          QuestionId = aq.questionId,
                          QuestionTitle = aq.question.Title,
                           
                          Choices = aq.question.Choices
                              .OrderBy(c => aq.ChoiceOrder.IndexOf(c.Id))
                              .Select(c => new choicedto
                              {
                                  ChoiceId = c.Id,
                                  Text = c.Text
                              }).ToList()
                      }).ToList()
            };

            return dto;
        }
    }
}
