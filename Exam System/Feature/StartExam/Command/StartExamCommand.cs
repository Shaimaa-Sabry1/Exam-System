using Exam_System.Feature.StartExam.DTO;
using MediatR;

namespace Exam_System.Feature.StartExam.Command
{
    public class StartExamCommand : IRequest<AttemptDto>
    {
        public int ExamId { get; set; }
        public int UserId { get; set; }
    }
}
