
using Exam_System.Feature.Exams.Model;
using Exam_System.Feature.Users.Model;

namespace Exam_System.Feature.Answers.Model
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public int Score { get; set; }

        // Navigation
        public User User { get; set; }
        public Exam Exam { get; set; }

        public ICollection<AnswerDetail> Details { get; set; } = new List<AnswerDetail>();
    }
}
