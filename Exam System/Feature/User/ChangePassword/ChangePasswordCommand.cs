using MediatR;

namespace Exam_System.Feature.User.ChangePassword
{
    public record ChangePasswordCommand(int UserId, string NewPassword) : IRequest;
}
