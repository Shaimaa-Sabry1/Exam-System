using Exam_System.Feature.Question.AddQuestion.Dtos;

namespace Exam_System.Feature.Question.GetAllQuestions.Dtos
{
    public class GetAllQuestionsDto
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // e.g., "Multiple Choice", "Single Choice"
        public ICollection<ChoiceToReturnDto> Choices { get; set; } = new List<ChoiceToReturnDto>();
    }
}
