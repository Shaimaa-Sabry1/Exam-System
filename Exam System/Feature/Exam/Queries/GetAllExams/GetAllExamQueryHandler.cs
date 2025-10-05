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
            var today = DateTime.Today;

            var query = _dbContext.Exams.AsQueryable();
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(e => e.Title.ToLower()==request.Search.ToLower());
            }


            var totalcount = await query.CountAsync();
            var exams = await query
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
                    Title= request.Title,
                    IsActive= (request.StartDate<=today&&request.EndDate>= today)
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
