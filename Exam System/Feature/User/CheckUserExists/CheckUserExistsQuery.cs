using Exam_System.Domain.Entities;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.User.CheckUserExists
{
    public record CheckUserExistsQuery(IFilterSpecification<Exam_System.Domain.Entities.User> specification) : IRequest<(bool isExist, Exam_System.Domain.Entities.User? user)>;

}
