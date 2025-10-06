namespace Exam_System.Feature.Question.AddQuestion.Dtos
{
    public class ChoiceToReturnDto
    {
        public string Text { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty ;
        public bool IsCorrect { get; set; }
    }
}
