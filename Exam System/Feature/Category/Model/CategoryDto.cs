namespace Exam_System.Feature.Category.Model
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string? Icon { get; set; }
        public int IsActiveExamCount { get; set; }
    }
}
