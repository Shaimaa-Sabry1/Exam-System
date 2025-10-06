using Exam_System.Feature.StartExam.DTO;
using MediatR;

namespace Exam_System.Feature.StartExam.Query
{
    public class GetAttemptQuery : IRequest<AttemptDto>
    {
        public int AttemptId { get; set; }
        public int UserId { get; set; } = default!;
    }
}
