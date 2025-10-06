namespace Exam_System.Domain.Entities
{
    public class AttemptQuestionChoice
    {
        public int Id { get; set; }
        public int AttemptQuestionId { get; set; }
        public int ChoiceId { get; set; }
        public int Order { get; set; }
        public bool? IsSelected { get; set; }          // Whether student picked it (useful for multi-select)
        public bool? IsCorrect { get; set; }         //Whether it’s the right answer (for scoring)

        public AttemptQuestion AttemptQuestion { get; set; } = default!;
        public Choice Choice { get; set; } = default!;
    }
}