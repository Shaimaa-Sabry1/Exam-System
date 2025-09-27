namespace Exam_System.Domain.Entities
{
    public class AnswerDetail
    {
        public int AnswerDetailId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int ChoiceId { get; set; }

        public  Answer Answer { get; set; }
        public Question Question { get; set; }
        public  Choice Choice { get; set; }
    }
}
