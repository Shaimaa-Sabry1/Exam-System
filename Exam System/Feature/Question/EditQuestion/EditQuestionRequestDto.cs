namespace Exam_System.Domain.Entities
{
    public class EditQuestionRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<ChoiceDto> Choices { get; set; } = new(); //Don't Appear in Swagger
    }
}