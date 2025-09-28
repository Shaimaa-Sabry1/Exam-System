using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Categories.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, object>
    {
        private readonly ExamDbcontext _dbContext;

        public GetAllCategoryQueryHandler(ExamDbcontext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<object> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var totalCount=await _dbContext.Categories.CountAsync(cancellationToken);
            var categories = await _dbContext.Categories
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Title = c.Title,
                    Icon = c.Icon,
                })
                .ToListAsync(cancellationToken);
            return new
            {
                Items = categories,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

        }
    }
}
