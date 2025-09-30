using MediatR;

namespace Exam_System.Feature.User.CheckUserExists
{
    public record CheckUserExistsByUserNameQuery(string UserName) : IRequest<bool>;
}
