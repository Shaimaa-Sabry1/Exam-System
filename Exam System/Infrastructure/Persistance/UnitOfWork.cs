using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;

namespace Exam_System.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamDbcontext _context;

        public ICategoryRepository Categories { get; }

        public IExamRepository Exam { get; }

        public UnitOfWork(ExamDbcontext context,ICategoryRepository categoryRepository,IExamRepository examRepository)
        {
          
            this._context = context;
            Categories = categoryRepository;
            Exam = examRepository;

        }
        public Task<int> SaveChangesAsync()=> _context.SaveChangesAsync();

    }
}
