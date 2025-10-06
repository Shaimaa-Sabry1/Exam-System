using System.Text.Json;

namespace Exam_System.Domain.Entities
{
    public class AttemptQuestion
    {
        public int attemptQuestionId { get; set; }
        public int startExamId { get; set; }
        public int questionId { get; set; }
        public int order { get; set; }
        public string ChoiceOrderJson { get; set; } = "[]";
        public List<int> ChoiceOrder
        {
            get => string.IsNullOrWhiteSpace(ChoiceOrderJson)
                        ? new List<int>()
                        : JsonSerializer.Deserialize<List<int>>(ChoiceOrderJson) ?? new List<int>();
            set => ChoiceOrderJson = JsonSerializer.Serialize(value);
        }
        public int? SelectedChoiceId { get; set; }  
        public bool? IsCorrect { get; set; }
        public StartExam startExam { get; set; }
        public Question question { get; set; }
        public ICollection<AttemptQuestionChoice> AttemptQuestionChoices { get; set; } = new List<AttemptQuestionChoice>();

    }
}
