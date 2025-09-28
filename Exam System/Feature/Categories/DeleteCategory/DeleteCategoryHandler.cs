using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Categories.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
          
            var category =await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if(category == null) 
            {
                return false;
                
            }
            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
