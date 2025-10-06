//using Exam_System.Domain.Entities;
//using Exam_System.Shared.Interface;
//using System.Linq.Expressions;

//namespace Exam_System.Shared.Specification
//{
//    public abstract class BaseSpecifications<T> : ISpecifications<T>
//    {
//        #region Criteria
//        public Expression<Func<T, bool>>? Criteria { get; private set; }
//        public BaseSpecifications(Expression<Func<T, bool>>? CriteriaExpression)
//        {
//            Criteria = CriteriaExpression;
//        }
//        #endregion

//        #region IncludeExpressions
//        public List<Expression<Func<T, object>>> IncludeExpressions { get; private set; } = [];

//        protected void AddInclude(Expression<Func<T, object>> includeExpression) => IncludeExpressions.Add(includeExpression);

//        #endregion
//    }
//}
