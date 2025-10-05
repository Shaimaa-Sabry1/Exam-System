using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.User.ResetPassword
{
    public record ResetPasswordCommand(string PasswordToken, string NewPassword, string ConfirmPassword) : IRequest<ResponseResult<string>>;
}
