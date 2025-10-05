using Exam_System.Domain.Entities;

namespace Exam_System.Feature.Question.AddQuestion
{
    public class AddQuestionToReturnDto
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int ExamId { get; set; }
        public List<ChoiceDto> Choices { get; set; } = new();
    }
}
