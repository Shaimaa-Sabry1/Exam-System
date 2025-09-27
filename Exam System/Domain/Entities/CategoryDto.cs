namespace Exam_System.Domain.Entities
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string? Icon { get; set; }
        public int IsActiveExamCount { get; set; }
    }
}
