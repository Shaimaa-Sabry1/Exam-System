using Exam_System.Domain.Entities;
using MediatR;

namespace Exam_System.Feature.User.CheckUserExists
{
    public record CheckUserExistsByEmailQuery(string Email) : IRequest<(bool, Exam_System.Domain.Entities.User?)>;

}
