namespace Exam_System.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // e.g., "Multiple Choice", "Single Choice"

        public int ExamId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Exam Exam { get; set; } = default!;
        public ICollection<Choice> Choices { get; set; } = new List<Choice>();

    }
}
