using Exam_System.Feature.Categories.Commands;
using Exam_System.Feature.Categories.Model;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Categories.Handlers
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
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
            return category.CategoryId;
        }
    }
}
