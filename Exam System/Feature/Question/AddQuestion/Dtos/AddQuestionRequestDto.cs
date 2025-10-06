namespace Exam_System.Feature.Question.AddQuestion.Dtos
{
    public class AddQuestionRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int ExamId { get; set; }
        public List<ChoiceDto> Choices { get; set; } = new();
    }
}

