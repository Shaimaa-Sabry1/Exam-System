using Exam_System.Domain.Entities;

using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Categories.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category =new Category
            {
                
                Title = request.Title,
                Icon = request.Icon,
                CreatedAt = DateTime.Now

            };
             await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return category.Id;
        }
    }
}
