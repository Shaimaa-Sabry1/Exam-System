using MediatR;

namespace Exam_System.Feature.Categories.DeleteCategory
{
    public class DeleteCategoryCommand:IRequest<bool>
    {
        public int CategoryId { get; set; }
    }
}
