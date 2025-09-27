using Exam_System.Domain.Entities;
using MediatR;
namespace Exam_System.Feature.Categories.GetById
{
    public class GetCategoryByIdQuery:IRequest<Category?>
    {
        public int Id { get; set; }
    }
}
