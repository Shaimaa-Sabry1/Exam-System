using Exam_System.Domain.Entities;
using Exam_System.Shared.Interface;
using System.Linq.Expressions;

namespace Exam_System.Shared.Specification
{
    public class UserByUserNameSpecification : IFilterSpecification<User>
    {
        private readonly string _username;

        public UserByUserNameSpecification(string username) {
            _username = username;
        }
        public Expression<Func<User, bool>> Criteria => user => user.UserName == _username;
    }
}
