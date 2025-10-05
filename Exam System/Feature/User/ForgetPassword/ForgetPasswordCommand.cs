using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.User.ForgetPassword
{
    public record ForgetPasswordCommand(string Email) : IRequest<ResponseResult<ForgetPasswordResponse>>;
}
