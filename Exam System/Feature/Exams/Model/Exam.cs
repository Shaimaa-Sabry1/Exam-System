using Exam_System.Feature.Answers.Model;
using Exam_System.Feature.Categories.Model;
using Exam_System.Feature.Questions.Model;

namespace Exam_System.Feature.Exams.Model
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public string? Icon { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
