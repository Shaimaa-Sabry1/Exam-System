namespace Exam_System.Feature.Question.GetAllQuestions
{
    public class GetAllQuestionsResponse
    {
        public IEnumerable<GettAllQuestionsDto> Questions { get; set; }
        public int TotalCount { get; set; }
    }

}
