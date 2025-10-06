namespace Exam_System.Feature.StartExam.DTO
{
    public class AttemptDto
    {
        public int AttemptId { get; set; }
        public string ExamTitle { get; set; } 
        public int DurationInMinutes { get; set; }
        
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int? ScoreTotal { get; set; }


        public List<AttemptQuestionDto> Questions { get; set; } = new();

    }
}
