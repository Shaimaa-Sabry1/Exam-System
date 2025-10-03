using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Exam.Queries.GetAllExams
{
    public class GetAllExamQueryHandler : IRequestHandler<GetAllExamQuery, GetAllExamResponse>
    {
        private readonly ExamDbContext _dbContext;

        public GetAllExamQueryHandler(ExamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<GetAllExamResponse> Handle(GetAllExamQuery request, CancellationToken cancellationToken)
        {
            var totalcount = await _dbContext.Exams.CountAsync();
            var exams = await _dbContext.Exams
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(request => new ExamDto
                {
                    ExamId = request.Id,
                    DurationInMinutes = request.DurationInMinutes,
                    EndDate = request.EndDate,
                    StartDate = request.StartDate,
                    CreatedAt = request.CreatedAt,
                    Icon= request.Icon,
                    Title= request.Title
                }).ToListAsync();
            return new GetAllExamResponse 
            {
                Exams = exams,
                TotalCount = totalcount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            


        }
    }
}
