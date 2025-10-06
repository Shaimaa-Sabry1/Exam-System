using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Exam_System.Infrastructure.Repositories
{
    public class QuestionRepository : GenaricRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ExamDbContext dbcontext) : base(dbcontext)
        {
            
        }
        public async Task<Question> GetQuestionsByIdWithChoicesAsync(int QuestionId)
        {
            var question =await _dbcontext.Questions
                                   .Where(Q => Q.Id == QuestionId)
                                   .Include(Q => Q.Choices)
                                   .FirstOrDefaultAsync();
            return question;
        }  
     
        public async Task<(IEnumerable<Question>, int)> GetAllQuestionsAsync(IFilterSpecification<Question> criteria)
        {
            var query = _dbcontext.Questions
                                   .Where(criteria.Criteria)
                                   .Include(Q => Q.Choices)
                                   .AsQueryable();
            var totalCount = await query.CountAsync();
            var Questions = await query.ToListAsync();
            return (Questions, totalCount);
        }


    }
}
