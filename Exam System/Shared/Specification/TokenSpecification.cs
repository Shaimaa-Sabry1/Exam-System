using Exam_System.Domain.Entities;
using Exam_System.Shared.Interface;
using System.Linq.Expressions;

namespace Exam_System.Shared.Specification
{
    public class TokenSpecification : IFilterSpecification<UserToken>
    {
        private readonly string _token;

        public TokenSpecification(string token)
        {
            _token = token;
        }
        public Expression<Func<UserToken, bool>> Criteria => token => token.Token == _token;
    }
}
