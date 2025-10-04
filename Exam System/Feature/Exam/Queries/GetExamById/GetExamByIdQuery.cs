using Exam_System.Domain.Entities;
using MediatR;

namespace Exam_System.Feature.Exam.Queries.GetExamById
{
    public record GetExamByIdQuery(int Id) : IRequest<Domain.Entities.Exam>;
   
}
