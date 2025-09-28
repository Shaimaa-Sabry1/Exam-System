using Exam_System.Feature.Exams.Model;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;

namespace Exam_System.Infrastructure.Repositories
{
    public class ExamRepository : GenaricRepository<Exam>, IExamRepository
    {
        public ExamRepository(ExamDbcontext context) :base(context)
        {
        }
    }
}
