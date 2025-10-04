using Exam_System.Domain.Entities;
using MediatR;

namespace Exam_System.Feature.Categories.GetAllCategory
{
    public class GetAllCategoryQuery:IRequest<CategoryPagedResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
