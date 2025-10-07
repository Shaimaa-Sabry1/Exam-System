using Exam_System.Feature.StartExam.Orchestrator.StartExam;

namespace Exam_System.Feature.StartExam.GetAttembt.DTO
{
    public class QuestionAttembtDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<ChoiceResponseDTO> Choices { get; set; } = new List<ChoiceResponseDTO>();
    }
}
