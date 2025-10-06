using Exam_System.Domain.Entities;
using Exam_System.Shared.Interface;
using System.Linq.Expressions;

namespace Exam_System.Shared.Specification
{
    public class QuestionsByQuestionNameSpecification(string questionName):IFilterSpecification<Question>
    {
        public Expression<Func<Question, bool>> Criteria => question => question.Title == questionName;

    }
}
