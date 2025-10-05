namespace Exam_System.Domain.Entities
{
    public class ChoiceDto
    {
        //public int ChoiceId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string ImageURL {  get; set; }= string.Empty;
        public bool IsCorrect { get; set; }
    }
}
