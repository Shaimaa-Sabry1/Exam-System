using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Categories.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, CategoryPagedResponse>
    {
        private readonly ExamDbContext _dbContext;

        public GetAllCategoryQueryHandler(ExamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
       

        async Task<CategoryPagedResponse> IRequestHandler<GetAllCategoryQuery, CategoryPagedResponse>.Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _dbContext.Categories.CountAsync(cancellationToken);
            var categories = await _dbContext.Categories
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Icon = c.Icon,
                    CreatedAt = c.CreatedAt

                })
                .ToListAsync(cancellationToken);
            return new CategoryPagedResponse
            {
                Categories= categories,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            }
            ;
        }
    }
}
