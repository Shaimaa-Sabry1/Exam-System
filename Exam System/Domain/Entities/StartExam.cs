namespace Exam_System.Domain.Entities
{
    public class StartExam
    {
        public int attemptId { get; set; }
        public int examId { get; set; }
        public int userId { get; set; }
        public DateTime startTime { get; set; } = DateTime.Now;
        public DateTime? endTime { get; set; }
        public int ? score { get; set; }
        public int? DurationTakenMinutes { get; set; }
        public Exam Exam { get; set; }
        public ICollection<AttemptQuestion> AttemptQuestions { get; set; } = new List<AttemptQuestion>();





    }
}
