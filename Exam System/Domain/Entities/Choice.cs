namespace Exam_System.Domain.Entities
{
    public class Choice
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? ImageURL { get; set; } 
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } 

    }
}
