using FluentValidation;

namespace Exam_System.Feature.User.RegisterUser
{
    public class RegisterCommandValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().Matches("^[a-zA-Z0-9_]+$")
                .WithMessage("UserName is not valid");

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(x => x.FirstName)
                .NotEmpty().Matches("^[A-Za-z]+$")
                .WithMessage("First name must contain only letters");

            RuleFor(x => x.LastName)
                .NotEmpty().Matches("^[A-Za-z]+$")
                .WithMessage("Last name must contain only letters");

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter")
                .Matches("[0-9]").WithMessage("Password must contain a number");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
