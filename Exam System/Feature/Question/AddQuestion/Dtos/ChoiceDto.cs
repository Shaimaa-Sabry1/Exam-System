namespace Exam_System.Feature.Question.AddQuestion.Dtos
{
    public class ChoiceDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        //public IFormFile? Image { get; set; }
        public bool IsCorrect { get; set; }
    }
}
