using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;

public class ExamRepository : GenaricRepository<Exam>, IExamRepository
{
    public ExamRepository(ExamDbContext context) : base(context)
    {
        
    }
}
