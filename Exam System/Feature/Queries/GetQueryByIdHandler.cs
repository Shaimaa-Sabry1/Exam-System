using Exam_System.Domain.Entities;
using Exam_System.Domain.Exception;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Queries
{
    public class GetQueryByIdHandler : IRequestHandler<GetByIdQuery, Domain.Entities.Exam>
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public GetQueryByIdHandler(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
        public async Task<Domain.Entities.Exam> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
           var exam = await _unitOfWork.Exam.GetByIdAsync(request.Id);
            if (exam == null) 
            {
                throw new ExamNotFoundException(request.Id);
            }
              return exam;

        }
    }
}
