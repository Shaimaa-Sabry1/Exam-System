using Exam_System.Feature.Exams.Commands;
using Exam_System.Feature.Exams.Model;
using Exam_System.Shared.Interface;
using FluentValidation;
using MediatR;

namespace Exam_System.Feature.Exams.Handlers
{
    public class CreateExamCommandHadler : IRequestHandler<CreateExamCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateExamCommand> _validator;

        public CreateExamCommandHadler(IUnitOfWork unitOfWork , IValidator<CreateExamCommand>validator)
        {
            _unitOfWork = unitOfWork;
            this._validator = validator;
        }

        public async Task<int> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var exam = new Exam() {
                 Title=request.Title,
                 Icon =request.Icon,
                 CategoryId=request.CategoryId,
                 DurationInMinutes=request.DurationInMinutes,
                 CreatedAt=DateTime.UtcNow,
                 StartDate=request.StartDate,
                 EndDate=request.EndDate   
            };
            await _unitOfWork.Exam.AddAsync(exam);
            await _unitOfWork.SaveChangesAsync();
            return exam.ExamId;
        }
    }
}
