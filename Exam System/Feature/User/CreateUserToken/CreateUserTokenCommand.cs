using MediatR;

namespace Exam_System.Feature.User.CreateUserToken
{
    public record CreateUserTokenCommand(int UserId, string Token) : IRequest
    {
    }
}
