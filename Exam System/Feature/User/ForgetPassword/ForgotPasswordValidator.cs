using FluentValidation;

namespace Exam_System.Feature.User.ForgetPassword
{
    public class ForgotPasswordValidator : AbstractValidator<ForgetPasswordCommand>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress()
                .WithMessage("Invalid email address");
        }
    }
}
