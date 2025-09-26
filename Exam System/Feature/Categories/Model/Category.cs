using Exam_System.Feature.Exams.Model;

namespace Exam_System.Feature.Categories.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string? Icon { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Exam> Exam { get; set; } = new List<Exam>();
    }
}
