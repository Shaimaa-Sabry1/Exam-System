using MediatR;

namespace Exam_System.Feature.Categories.Commands
{
    public class AddCategoryCommand:IRequest<int>
    {
        public string Title { get; set; }
        public string? Icon{ get; set; }
    }
}
