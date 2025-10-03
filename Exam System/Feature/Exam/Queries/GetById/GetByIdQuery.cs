using Exam_System.Domain.Entities;
using MediatR;

namespace Exam_System.Feature.Exam.Queries.GetById
{
    public record GetByIdQuery(int Id) : IRequest<Domain.Entities.Exam>;
   
}
