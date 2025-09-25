using Exam_System.Feature.Exams.Model;

namespace Exam_System.Feature.Questions.Model
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }  // e.g., "Multiple Choice", "Single Choice"

        public int ExamId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Exam Exam { get; set; } = default!;
        public ICollection<Choice> Choices { get; set; } = new List<Choice>();

    }
}
