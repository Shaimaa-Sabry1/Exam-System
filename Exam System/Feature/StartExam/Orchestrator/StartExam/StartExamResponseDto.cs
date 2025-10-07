using Exam_System.Feature.StartExam.GetAttembt.DTO;

namespace Exam_System.Feature.StartExam.Orchestrator.StartExam
{
    public class StartExamResponseDto
    {
        public int AttembtId { get; set; }
        public string Tiltle { get; set; }
        public int QuestionsCount { get; set; }
        public int DurationInMinutes { get; set; }
        public List<QuestionAttembtDto> Questions { get; set; }

    }
}
