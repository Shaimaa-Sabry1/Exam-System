using MediatR;

namespace Exam_System.Feature.Exams.Commands
{
    public class  CreateExamCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string? Icon { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }

    }
}
