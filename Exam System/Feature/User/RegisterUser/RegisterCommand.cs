using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.User.RegisterUser
{
    public record RegisterCommand(string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword,
    string PhoneNumber) : IRequest<ResponseResult<string>>;
}
