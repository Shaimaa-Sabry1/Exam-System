//namespace Exam_System.Shared.Specification
//{
//    internal static class SpecificationEvaluator
//    {
//        //create query
//        //_dbcontext.products.where(p=>p.id==id).include(p=>p.productbrand).include(p=>p.producttype)
//        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery , ISpecifications<TEntity , TKey> specifications) where TEntity : BaseEntity<TKey>
//        {
//            //start build query 
//            var Query = InputQuery;

//            #region where

//            if (specifications.Criteria is not null)
//            {
//                Query = Query.Where(specifications.Criteria);
//            }

//            #endregion

//            #region Include
//            var IncludeExpressionsCount = specifications.IncludeExpressions.Count;
//            if (specifications.IncludeExpressions is not null && IncludeExpressionsCount > 0)
//            {
//                //foreach (var IncludeExpression in specifications.IncludeExpressions)                
//                //{
//                //    Query=Query.Include(IncludeExpression);
//                //}

//                /*                    Another Way                   */
//                //_dbcontext.products.where()
//                //_dbcontext.products.where().Include()
//                //_dbcontext.products.where().Include().Include
//                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
//            }
//            #endregion

//            return Query;
//        }
//    }
//}
