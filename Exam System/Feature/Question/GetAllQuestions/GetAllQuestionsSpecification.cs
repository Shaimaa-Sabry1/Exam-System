//using Exam_System.Shared.Specification;

//namespace Exam_System.Feature.Question.GetAllQuestions
//{
//    public class GetAllQuestionsSpecification : BaseSpecifications<Domain.Entities.Question>
//    {
//        public GetAllQuestionsSpecification(string QuestionName) : base(Q => Q.Title.ToLower()==QuestionName.ToLower())
//        {
//            AddInclude(Q => Q.Choices);
//        }
//        public GetAllQuestionsSpecification(int ExamId) : base(Q => Q.ExamId == ExamId )
//        {
//            AddInclude(Q => Q.Choices);
//        }
//    }
//}
