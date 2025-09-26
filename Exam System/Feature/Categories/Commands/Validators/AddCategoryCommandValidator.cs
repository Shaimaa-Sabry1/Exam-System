using FluentValidation;

namespace Exam_System.Feature.Categories.Commands.Validators
{
    public class AddCategoryCommandValidator: AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).
                WithMessage("Title must not exceed 100 characters.");
        }
    }
}
