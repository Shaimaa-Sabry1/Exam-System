namespace Exam_System.Feature.Question.AddQuestion.Dtos
{
    public class AddQuestionToReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int ExamId { get; set; }
        public List<ChoiceToReturnDto> Choices { get; set; } = new();
    }
}
