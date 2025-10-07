using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Answer.SubmitExam.Commands
{
    public record MarkAttemptAsSubmittedCommand(int AttemptId) : IRequest<bool>;

    public class MarkAttemptAsSubmittedCommandHandler : IRequestHandler<MarkAttemptAsSubmittedCommand, bool>
    {
        private readonly GenaricRepository<attembt> _attemptRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAttemptAsSubmittedCommandHandler(
            GenaricRepository<attembt> attemptRepository,
            IUnitOfWork unitOfWork)
        {
            _attemptRepository = attemptRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(MarkAttemptAsSubmittedCommand request, CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.GetByIdAsync(request.AttemptId);
            if (attempt == null)
            {
                return false;
            }

            attempt.IsSubmitted = true;
            await _attemptRepository.UpdateAsync(attempt);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}

