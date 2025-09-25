namespace Exam_System.Feature.Questions.Model
{
    public class QuestionsDto
    {
        public Guid QuestionId { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }
        public List<Choice> Choices { get; set; } = new();
    }
}
