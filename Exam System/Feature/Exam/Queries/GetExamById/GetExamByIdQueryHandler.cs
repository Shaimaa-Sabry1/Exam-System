using Exam_System.Domain.Entities;
using Exam_System.Domain.Exception;
using Exam_System.Shared.Interface;
using MediatR;


namespace Exam_System.Feature.Exam.Queries.GetExamById
{
    public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, Domain.Entities.Exam>
    {

        private readonly IUnitOfWork _unitOfWork;


        public GetExamByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
        public async Task<Domain.Entities.Exam> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
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
