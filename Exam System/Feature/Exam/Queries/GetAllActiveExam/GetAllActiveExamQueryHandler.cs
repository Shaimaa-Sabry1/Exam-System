using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Exam.Queries.GetAllActiveExam
{
    public class GetAllActiveExamQueryHandler : IRequestHandler<GetAllActiveQuery, GetAllExamResponse>
    {
        private readonly GenaricRepository<Domain.Entities.Exam> _examrepo;

        public GetAllActiveExamQueryHandler(GenaricRepository<Exam_System.Domain.Entities.Exam> examrepo)
        {
            this._examrepo = examrepo;
        }
        public async Task<GetAllExamResponse> Handle(GetAllActiveQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.Today;
            var exam =  _examrepo.GetAll();
            exam = exam.Where(e => e.StartDate <= today && e.EndDate >= today);

            var activeExams = exam.ToList();
            var totalcount = activeExams.Count;
            var exams = activeExams
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(request => new ExamDto
                {
                    ExamId = request.Id,
                    DurationInMinutes = request.DurationInMinutes,
                    EndDate = request.EndDate,
                    StartDate = request.StartDate,
                    CreatedAt = request.CreatedAt,
                    Icon = request.Icon,
                    Title = request.Title,
                    IsActive = (request.StartDate <= today && request.EndDate >= today)
                }).ToList();

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
