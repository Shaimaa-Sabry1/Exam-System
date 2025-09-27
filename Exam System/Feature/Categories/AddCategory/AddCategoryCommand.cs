using MediatR;

namespace Exam_System.Feature.Categories.AddCategory
{
    public class AddCategoryCommand:IRequest<int>
    {
        public string Title { get; set; }
        public string? Icon{ get; set; }
    }
}
