using Exam_System.Domain.Exception;
using Exam_System.Feature.Queries;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Exam.UpdateExam
{
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExamCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            this._mediator = mediator;
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _mediator.Send(new GetByIdQuery(request.Id));

            if (exam == null)
            {
                throw new ExamNotFoundException(request.Id);
            }
            exam.Title = request.Title;
            exam.Icon = request.Icon;
            exam.DurationInMinutes = request.DurationInMinutes;
            exam.StartDate = request.StartDate;
            exam.EndDate = request.EndDate;
            



            await _unitOfWork.Exam.UpdateAsync(exam);
            var result= await _unitOfWork.SaveChangesAsync();
           if(result ==0)
                return false;
            return true;



        }
    }
}
