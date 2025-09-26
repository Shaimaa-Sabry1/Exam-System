using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;

namespace Exam_System.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamDbcontext _context;

        public ICategoryRepository Categories { get; }
        

        public UnitOfWork(ExamDbcontext context,ICategoryRepository categoryRepository)
        {
          
            this._context = context;
            Categories = categoryRepository;
        }
        public Task<int> SaveChangesAsync()=> _context.SaveChangesAsync();

    }
}
