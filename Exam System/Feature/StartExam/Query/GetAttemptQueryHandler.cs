using Exam_System.Feature.StartExam.DTO;
using Exam_System.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.StartExam.Query
{
    public class GetAttemptQueryHandler : IRequestHandler<GetAttemptQuery, AttemptDto>
    {
        private readonly GenaricRepository<Domain.Entities.StartExam> _repository;

        public GetAttemptQueryHandler(GenaricRepository<Exam_System.Domain.Entities.StartExam>repository)
        {
            this._repository = repository;
        }
        public async Task<AttemptDto> Handle(GetAttemptQuery request, CancellationToken cancellationToken)
        {
            var attempt = _repository.GetAll()
                                                      .Include(a => a.Exam)
                                                      .Include(a => a.AttemptQuestions)
                                                      .ThenInclude(aq => aq.question)
                                                      .ThenInclude(q => q.Choices)
                                                      .FirstOrDefault(a => a.attemptId == request.AttemptId && a.userId == request.UserId);
            if (attempt == null)
                throw new Exception("Attempt not found");
            var attemptDto = new AttemptDto
            {
                AttemptId = attempt.attemptId,
                
                ExamTitle = attempt.Exam.Title,
                StartedAt = attempt.startTime,
                Questions = attempt.AttemptQuestions.Select
                   (aq => new AttemptQuestionDto 
                   {
                       AttemptQuestionId = aq.attemptQuestionId,
                       QuestionId = aq.questionId,
                       QuestionTitle = aq.question.Title,
                        
                       SelectedChoiceId = aq.SelectedChoiceId,
                       Choices = aq.question.Choices
                            
                            .OrderBy(c => aq.ChoiceOrder.IndexOf(c.Id))
                            .Select(c => new choicedto
                            {
                                ChoiceId = c.Id,
                                Text = c.Text
                            }).ToList()
                   }).ToList()
            };
            return attemptDto;






        }
    }
    
}
