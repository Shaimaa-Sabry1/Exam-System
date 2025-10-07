using Exam_System.Domain.Exception;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.StartExam.Delete_attembt
{
    public record DeleteAttembtCommand(int AttembtId) : IRequest<bool>;

    public class DeleteAttembtCommandHandler : IRequestHandler<DeleteAttembtCommand, bool>
    {
        private readonly GenaricRepository<Domain.Entities.attembt> _attembtRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAttembtCommandHandler(GenaricRepository<Domain.Entities.attembt> attembtRepository,IUnitOfWork unitOfWork)
        {
            _attembtRepository = attembtRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteAttembtCommand request, CancellationToken cancellationToken)
        {
            var attembt = await _attembtRepository.GetByIdAsync(request.AttembtId);
            if (attembt == null)
            {
                throw new NotFoundException($"Attembt with Id {request.AttembtId} not found "); 
            }
            await _attembtRepository.DeleteAsync(attembt);
           var result =  await _unitOfWork.SaveChangesAsync();
              if(result == 0)
                  return false;

            return true;
        }
    }


}
