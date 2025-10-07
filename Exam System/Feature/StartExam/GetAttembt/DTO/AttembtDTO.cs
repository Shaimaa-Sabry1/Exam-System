using Exam_System.Domain.Entities;
using Exam_System.Feature.Question.GetAllQuestions.Dtos;

namespace Exam_System.Feature.StartExam.GetAttembt.DTO
{
    public class AttembtDTO
    {

       public int attembtId { get; set; }
        public int ExamId { get; set; }
        public int userId { get; set; }
        public string Tiltle { get; set; }
        public int QuestionsCount { get; set; }
        public int DurationInMinutes { get; set; }
        public List<QuestionAttembtDto> Questions { get; set; }
    }
}
