namespace Exam_System.Feature.Question.GetAllQuestions.Dtos
{
    public class GetAllQuestionsResponseDto
    {
        public IEnumerable<GetAllQuestionsDto> Questions { get; set; }
        public int TotalCount { get; set; }
    }

}
