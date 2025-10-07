namespace Exam_System.Feature.Answer.DTO
{
    public class CreateAnswerDetailCommandDto
    {
        // Fixed property name typo so model binding matches expected "QuestionId"
           
        public int QuestionId { get; set; } 
        public List<int> SelectedChoiceIds { get; set; } = new List<int>();
    }
}
