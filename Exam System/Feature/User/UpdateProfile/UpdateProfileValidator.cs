using FluentValidation;

namespace Exam_System.Feature.User.UpdateProfile
{
    public class UpdateProfileValidator:AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator() { 
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Old password is required");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New password is required").MinimumLength(6).WithMessage("New password must be at least 6 characters long");
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage("Confirm new password is required")
                .Equal(x => x.NewPassword).WithMessage("Confirm new password must match new password");

        }
    }
}
