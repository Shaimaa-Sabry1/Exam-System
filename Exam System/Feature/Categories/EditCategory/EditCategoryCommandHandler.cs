using Exam_System.Domain.Entities;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Categories.EditCategory
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, Category?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       

        Task<Category?> IRequestHandler<EditCategoryCommand, Category?>.Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.Categories.GetByIdAsync(request.Id);
            if (category == null) return null;
            category.Result.Title = request.Title;
            category.Result.Icon = request.Icon;
           

            _unitOfWork.Categories.UpdateAsync(category.Result);
            _unitOfWork.SaveChangesAsync();
            return category;

        }
    }
}
