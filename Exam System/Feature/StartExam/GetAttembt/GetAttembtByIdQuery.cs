using Exam_System.Domain.Entities;
using Exam_System.Feature.Question.GetAllQuestions.Dtos;
using Exam_System.Feature.StartExam.GetAttembt.DTO;
using Exam_System.Feature.StartExam.Orchestrator.StartExam;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Exam_System.Feature.StartExam.GetAttembt
{
    public record GetAttembtByIdQuery(int AttembtId) : IRequest<ResponseResult<GetAttembtByIdResponseDTO>>;

    public class GetAttembtByIdQueryHandler : IRequestHandler<GetAttembtByIdQuery, ResponseResult<GetAttembtByIdResponseDTO>>
    {
        private readonly GenaricRepository<attembt> _attembtRebository;
        private readonly GenaricRepository<Domain.Entities.Exam> _examRepository;

        public GetAttembtByIdQueryHandler(GenaricRepository<attembt>AttembtRebository,
            
            GenaricRepository<Domain.Entities.Exam>ExamRepository
          )
        {
            _attembtRebository = AttembtRebository;
           _examRepository = ExamRepository;
        }
        public async Task<ResponseResult<GetAttembtByIdResponseDTO>> Handle(GetAttembtByIdQuery request, CancellationToken cancellationToken)
        {
            var attembt = _attembtRebository.GetAll().FirstOrDefault(a=>a.Id==request.AttembtId);
            if (attembt == null) { return ResponseResult<GetAttembtByIdResponseDTO>.FailResponse("No atteembt found."); }
            var exam = _examRepository.GetAll()
                                                 .Include(a => a.Questions)
                                                 .ThenInclude(q => q.Choices)
                                                 .FirstOrDefault(a => a.Id == attembt.ExamId);
            if (exam == null) { return ResponseResult<GetAttembtByIdResponseDTO>.FailResponse("Exam not found or not active "); }

            var randomQuestions = exam.Questions
                .OrderBy(q => Guid.NewGuid())
                .ToList();


            var attembtDto = new AttembtDTO()
            {
                attembtId = request.AttembtId,
                ExamId = exam.Id,
                Tiltle = exam.Title,
                QuestionsCount = exam.Questions.Count(),
                DurationInMinutes = exam.DurationInMinutes,
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
            var response = new GetAttembtByIdResponseDTO() {AttembtDTO=attembtDto };
            return ResponseResult<GetAttembtByIdResponseDTO>.SuccessResponse(response);
        }
    }
}
