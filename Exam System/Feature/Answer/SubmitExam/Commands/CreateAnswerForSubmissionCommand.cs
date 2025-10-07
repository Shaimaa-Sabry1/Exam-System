using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Answer.SubmitExam.Commands
{
    public record CreateAnswerForSubmissionCommand(int UserId, int AttemptId) : IRequest<int>;

    public class CreateAnswerForSubmissionCommandHandler : IRequestHandler<CreateAnswerForSubmissionCommand, int>
    {
        private readonly GenaricRepository<Domain.Entities.Answer> _answerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAnswerForSubmissionCommandHandler(
            GenaricRepository<Domain.Entities.Answer> answerRepository,
            IUnitOfWork unitOfWork)
        {
            _answerRepository = answerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateAnswerForSubmissionCommand request, CancellationToken cancellationToken)
        {
            var answer = new Domain.Entities.Answer
            {
                UserId = request.UserId,
                attembtId = request.AttemptId,
                SubmittedAt = DateTime.Now,
                Score = 0 // Will be calculated later
            };

            await _answerRepository.AddAsync(answer);
            await _unitOfWork.SaveChangesAsync();

            return answer.Id;
        }
    }
}

