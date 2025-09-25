using Exam_System.Feature.Questions.Model;

namespace Exam_System.Feature.Answers.Model
{
    public class AnswerDetail
    {
        public Guid AnswerDetailId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public Guid ChoiceId { get; set; }

        public  Answer Answer { get; set; }
        public Question Question { get; set; }
        public  Choice Choice { get; set; }
    }
}
