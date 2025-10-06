using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;

namespace Exam_System.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamDbContext _context;

        public ICategoryRepository Categories { get; }

        public IExamRepository Exam { get; }
        public IQuestionRepository Question { get; }

        public UnitOfWork(ExamDbContext context, ICategoryRepository categoryRepository,IExamRepository exam ,IQuestionRepository question)
        {

            this._context = context;
            Categories = categoryRepository;
            Exam = exam;
            Question = question;

        }
        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

    }
}