using FluentValidation;

namespace Exam_System.Feature.Exams.Commands.Validations
{
    public class CreateExamCommandValidator : AbstractValidator<CreateExamCommand>
    {
        public CreateExamCommandValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title Is required.")
                .Length(3, 20).WithMessage("Title must be between 3 and 20 characters")
                .Matches("^[a-zA-Z ]+$").WithMessage("Title can only contain letters and spaces");
            RuleFor(x => x.Icon)
            .NotEmpty().WithMessage("Icon is required");

           // RuleFor(x => x.CategoryId)
           //.GreaterThan(0).WithMessage("CategoryId must be valid");

            RuleFor(x => x.StartDate)
            .Must(date => date >= DateTime.Today)
            .WithMessage("Start date must be today or in the future");

            RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date must be after or equal to start date");

        }
    }
}