using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Microsoft.EntityFrameworkCore;
namespace Exam_System.Infrastructure.Repositories
{
    public class QuestionRepository : GenaricRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ExamDbContext dbcontext) : base(dbcontext)
        {
            
        }
        public async Task<(IEnumerable<Question>, int)> GetAllQuestionsAsync(int ExamId)
        {
            var query = _dbcontext.Set<Question>()
                                   .Where(Q => Q.ExamId == ExamId)
                                   .Include(Q => Q.Choices)
                                   .AsQueryable();
            var totalCount =await query.CountAsync();
            var Questions = await query.ToListAsync();
            return (Questions, totalCount);
        }
        public async Task<(IEnumerable<Question>, int)> GetAllQuestionsAsync(string QuestionName)
        {
            var query = _dbcontext.Set<Question>()
                                   .Where(Q => Q.Title.ToLower().Contains(QuestionName.ToLower()))
                                   .Include(Q => Q.Choices)
                                   .AsQueryable();
            var totalCount = await query.CountAsync();
            var Questions = await query.ToListAsync();
            return (Questions, totalCount);
        }



    }
}
