using MediatR;

namespace Exam_System.Feature.Exam.DeleteExam
{
    public class DeleteExamCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
