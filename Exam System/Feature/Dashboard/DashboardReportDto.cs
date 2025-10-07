namespace Exam_System.Feature.Dashboard
{
    public class DashboardReportDto
    {
        public int TotalEams { get; set; }
        public int ActiveExam { get; set; }
        public int InActiveExam { get; set; }
        public List<ExamActivityDto> MostActiveExams { get; set; } = new();
        public List<CategoryActivityDto> MostActiveCategories { get; set; } = new();
        public List<DailyExamActivityDto> DailyExamActivity { get; set; } = new();
    }
}
