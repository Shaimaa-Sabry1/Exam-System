namespace Exam_System.Domain.Entities
{
    public class AnswerDetail
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public List<int> SelectedChoiceIds { get; set; } = new List<int>();

        public bool IsCorrect { get; set; }


        public Answer Answer { get; set; }
        public Question Question { get; set; }
       
      
    }
}
