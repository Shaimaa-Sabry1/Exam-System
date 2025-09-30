using System.Linq.Expressions;

namespace Exam_System.Shared.Interface
{
    public interface IFilterSpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }

}
