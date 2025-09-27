namespace Exam_System.Domain.Entities
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public string? Icon { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; }

    }
}
