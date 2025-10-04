using Exam_System.Domain.Exception;
using Exam_System.Feature.Exam.Queries;
using Exam_System.Feature.Exam.Queries;
using Exam_System.Feature.Exam.Queries.GetExamById;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Exam.DeleteExam
{
    public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExamCommandHandler(IMediator mediator , IUnitOfWork unitOfWork)
        {
            this._mediator = mediator;
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _mediator.Send(new GetExamByIdQuery(request.Id));
            if (exam == null) 
            {
                throw new ExamNotFoundException(request.Id);
            }

            await _unitOfWork.Exam.DeleteAsync(exam);
           var result =  await _unitOfWork.SaveChangesAsync();
              if(result == 0)
                 return false;
              return true;


        }
    }
}
