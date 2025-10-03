using FluentValidation;

namespace Exam_System.Feature.Exam.UpdateExam
{
    public class UpdateExamCommandValidator : AbstractValidator<UpdateExamCommand>
    {
        public UpdateExamCommandValidator()
        {
            RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("Exam Id must be valid");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .Length(3, 20).WithMessage("Title must be between 3 and 20 characters")
                .Matches("^[a-zA-Z ]+$").WithMessage("Title can only contain letters and spaces");

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required");

            

            RuleFor(x => x.StartDate)
                .Must(date => date >= DateTime.Today)
                .WithMessage("Start date must be today or in the future");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be after or equal to start date");

            RuleFor(x => x.DurationInMinutes)
                .InclusiveBetween(20, 180)
                .WithMessage("Duration must be between 20 and 180 minutes");
        }
    }
}
