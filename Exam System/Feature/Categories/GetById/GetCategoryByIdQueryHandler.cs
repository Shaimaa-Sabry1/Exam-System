using Exam_System.Domain.Entities;
using Exam_System.Shared.Interface;
using MediatR;
using Microsoft.Identity.Client;

namespace Exam_System.Feature.Categories.GetById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Categories.GetByIdAsync(request.Id);
        }
    }
}
