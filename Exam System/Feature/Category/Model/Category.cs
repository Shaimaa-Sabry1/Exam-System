namespace Exam_System.Feature.Category.Model
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string? Icon { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
