using Exam_System.Domain.Entities;

namespace Exam_System.Feature.Exam.Queries
{
    public class GetAllExamResponse
    {
        public List<ExamDto> Exams { get; set; } 
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
