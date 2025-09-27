using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;

namespace Exam_System.Infrastructure.Repositories
{
    public class CategoryRepository:GenaricRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ExamDbcontext context) : base(context)
        {
        }
    }
}
