using Exam_System.Domain.Entities;

namespace Exam_System.Feature.Question.GetAllQuestions
{
    public class GettAllQuestionsDto
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // e.g., "Multiple Choice", "Single Choice"
        public ICollection<ChoiceDto> Choices { get; set; } = new List<ChoiceDto>();
    }
}
