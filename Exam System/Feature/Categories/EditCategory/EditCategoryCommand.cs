using Exam_System.Domain.Entities;
using MediatR;

namespace Exam_System.Feature.Categories.EditCategory
{
    public class EditCategoryCommand:IRequest<Category?>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Icon{ get; set; }
    }
}
