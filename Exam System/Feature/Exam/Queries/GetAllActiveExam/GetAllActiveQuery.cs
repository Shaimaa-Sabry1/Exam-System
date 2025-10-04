using MediatR;

namespace Exam_System.Feature.Exam.Queries.GetAllActiveExam
{
    public class GetAllActiveQuery : IRequest<GetAllExamResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
