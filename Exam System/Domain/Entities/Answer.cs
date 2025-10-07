namespace Exam_System.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
          
        public int UserId { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public int Score { get; set; }
        public int attembtId { get; set; }

        // Navigation
        public User User { get; set; }
       
        public attembt Attembt { get; set; }

        public ICollection<AnswerDetail> Details { get; set; } = new List<AnswerDetail>();
    }
}
