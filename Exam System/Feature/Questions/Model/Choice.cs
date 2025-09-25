namespace Exam_System.Feature.Questions.Model
{
    public class Choice
    {
        public Guid ChoiceId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
