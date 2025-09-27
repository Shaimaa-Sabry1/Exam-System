using FluentValidation;

namespace Exam_System.Feature.Categories.EditCategory
{
    public class EditCategoryCommandValidator:AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid Category Id");
            RuleFor(c => c.Title).NotEmpty().WithMessage("Title is required")
                                 .MaximumLength(100).WithMessage("Title must not exceed 100 characters");
        }
    }
}
