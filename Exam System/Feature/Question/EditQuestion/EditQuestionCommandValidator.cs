using FluentValidation;

namespace Exam_System.Feature.Questions.EditQuestion
{
    public class EditQuestionCommandValidator : AbstractValidator<EditQuestionCommand>
    {
        public EditQuestionCommandValidator()
        {
            RuleFor(x => x.Title)
                       .NotEmpty().WithMessage("Title is required")
                       .MinimumLength(3).WithMessage("Title must be at least 3 characters long")
                       .MaximumLength(20).WithMessage("Title must not exceed 20 characters")
                       .Matches(@"^[a-zA-Z\s\?]+$").WithMessage("Title must contain only letters ,spaces and a question mark");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Please select a question type");

            RuleFor(x => x.Choices)
                .Must(c => c.Any(choice => choice.IsCorrect))
                .WithMessage("At least one correct answer is required");
        }
    }
}