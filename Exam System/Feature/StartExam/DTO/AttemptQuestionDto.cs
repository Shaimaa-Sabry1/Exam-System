namespace Exam_System.Feature.StartExam.DTO
{
    public class AttemptQuestionDto
    {
        public int AttemptQuestionId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionTitle  { get; set; } = default!;
        public int Order { get; set; }
        public List<choicedto> Choices { get; set; } = new();
        public int? SelectedChoiceId { get; set; }
    }
}