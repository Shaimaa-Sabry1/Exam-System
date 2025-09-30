using Exam_System.Domain.Entities;
using Exam_System.Shared.Interface;
using System.Linq.Expressions;

namespace Exam_System.Shared.Specification
{
    public class UserByEmailSpecification: IFilterSpecification<User>
    {
        private readonly string _email;

        public UserByEmailSpecification(string email)
        {
            _email = email;
        }
        public Expression<Func<User, bool>> Criteria => user => user.Email == _email;
    }
}
