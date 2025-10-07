using Exam_System.Domain.Entities;
using Exam_System.Domain.Exception;
using Exam_System.Feature.StartExam.GetAttembt.DTO;
using Exam_System.Feature.StartExam.Orchestrator.StartExam;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.StartExam.GetAttembt.Query
{
    public record GetAttembtByIdQuery(int AttembtId) : IRequest<AttembtDTO>;

    public class GetAttembtByIdQueryHandler : IRequestHandler<GetAttembtByIdQuery, AttembtDTO>
    {
        private readonly GenaricRepository<attembt> _attembtRepository;
        private readonly GenaricRepository<Exam_System.Domain.Entities.Exam> _examRepository;
        private readonly ExamDbContext _dbContext;

        public GetAttembtByIdQueryHandler(
            GenaricRepository<attembt> attembtRepository,
            GenaricRepository<Exam_System.Domain.Entities.Exam> examRepository,
            ExamDbContext dbContext)
        {
            _attembtRepository = attembtRepository;
            _examRepository = examRepository;
            _dbContext = dbContext;
        }

        public async Task<AttembtDTO> Handle(GetAttembtByIdQuery request, CancellationToken cancellationToken)
        {
            var attembt = await _attembtRepository.GetByIdAsync(request.AttembtId);

            if (attembt == null)
                throw new NotFoundException($"Attempt with ID {request.AttembtId} not found");

            var exam = await _dbContext.Exams
                .Include(e => e.Questions)
                .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(e => e.Id == attembt.ExamId, cancellationToken);

            if (exam == null)
                throw new NotFoundException($"Exam with ID {attembt.ExamId} not found");

            var randomQuestions = exam.Questions
                .OrderBy(q => Guid.NewGuid())
                .ToList();

            var attembtDto = new AttembtDTO
            {
                attembtId = attembt.Id,
                ExamId = exam.Id,
                userId = attembt.UserId,
                Tiltle = exam.Title,
                DurationInMinutes = exam.DurationInMinutes,
                QuestionsCount = exam.Questions.Count(),
                Questions = randomQuestions.Select(question => new QuestionAttembtDto
                {
                    Id = question.Id,
                    Title = question.Title,
                    Type = question.Type.ToString(),
                    Choices = question.Choices.Select(c => new ChoiceResponseDTO
                    {
                        Id = c.Id,
                        Text = c.Text,
                        ImageURL = string.Empty
                    }).ToList()
                }).ToList()
            };

            return attembtDto;
        }
    }
}

