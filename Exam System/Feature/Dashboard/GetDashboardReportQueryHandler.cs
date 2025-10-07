using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Dashboard
{
    public class GetDashboardReportQueryHandler : IRequestHandler<GetDashboardReportQuery, ResponseResult<DashboardReportDto>>
    {
        private readonly ExamDbContext _dbContext;

        public GetDashboardReportQueryHandler(ExamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<ResponseResult<DashboardReportDto>> Handle(GetDashboardReportQuery request, CancellationToken cancellationToken)
        {
            var now= DateTime.Now;
            var totalExam = await _dbContext.Exams.CountAsync(cancellationToken);
            var activeExam = await _dbContext.Exams.CountAsync(e => e.StartDate <= now && e.EndDate >= now, cancellationToken);
            var inactiveExam = totalExam - activeExam;
            var mostActiveExams = await _dbContext.Exams
                .OrderByDescending(e => e.Answers.Count)
                .Take(5)
                .Select(e => new ExamActivityDto
                {
                    Title = e.Title,
                    ParticipationCount = e.Answers.Count
                })
                .ToListAsync(cancellationToken);
            var mostActiveCategories = await _dbContext.Categories
                .OrderByDescending(c => c.Exam.Sum(e => e.Answers.Count))
                .Take(5)
                .Select(c => new CategoryActivityDto
                {
                    CategoryName= c.Title,
                    ExamCount = c.Exam.Sum(e => e.Answers.Count)
                })
                .ToListAsync(cancellationToken);
            var sevenDaysAgo = now.AddDays(-6);
            var dailyActivity = await _dbContext.Exams
                .Where(e => e.CreatedAt >= sevenDaysAgo)
                .GroupBy(e => e.CreatedAt.Date)
                .Select(g => new DailyExamActivityDto
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    ExamCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);
           var report = new DashboardReportDto
            {
                TotalEams = totalExam,
                ActiveExam = activeExam,
                InActiveExam = inactiveExam,
                MostActiveExams = mostActiveExams,
                MostActiveCategories = mostActiveCategories,
                DailyExamActivity = dailyActivity
            };
            return ResponseResult<DashboardReportDto>.SuccessResponse(report, "Dashboard report generated successfully ");
        }
    }
}
