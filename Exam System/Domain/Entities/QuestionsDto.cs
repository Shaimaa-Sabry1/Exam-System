namespace Exam_System.Domain.Entities
{
    public class QuestionsDto
    {
        public int QuestionId { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }
        public List<Choice> Choices { get; set; } = new();
    }
}
