using Exam_System.Domain.Entities;
using Exam_System.Feature.Exams.Commands;
using Exam_System.Service;
using Exam_System.Shared.Interface;
using FluentValidation;
using MediatR;

namespace Exam_System.Feature.Exams.Handlers
{
    public class CreateExamCommandHadler : IRequestHandler<CreateExamCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateExamCommand> _validator;
        private readonly IImageHelper _imageHelper;
        private readonly IConfiguration _configuration;

        public CreateExamCommandHadler(IUnitOfWork unitOfWork, IValidator<CreateExamCommand> validator,IImageHelper imageHelper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            this._validator = validator;
            this._imageHelper = imageHelper;
            this._configuration = configuration;
        }

        public async Task<int> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            string iconPath = null;


            if (request.Icon != null)
            {
                
                iconPath = await _imageHelper.UploadImageAsync (request.Icon, "exam-icons");
            }

            var exam = new Exam_System.Domain.Entities.Exam()
            {
                
                CategoryId= request.CategoryId,
                Title = request.Title,
                Icon = $"{_configuration["BaseUrl"]}/{iconPath}" ,
                DurationInMinutes = request.DurationInMinutes,
                CreatedAt = DateTime.UtcNow,
                StartDate = request.StartDate,
                EndDate = request.EndDate  
                
            };

            await _unitOfWork.Exam.AddAsync(exam);
            await _unitOfWork.SaveChangesAsync();
            return exam.Id;
        }
    }
}