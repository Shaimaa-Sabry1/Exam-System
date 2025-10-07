using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.Answer.SubmitExam.Commands
{
    public record UpdateAnswerScoreCommand(int AnswerId, int Score) : IRequest<bool>;

    public class UpdateAnswerScoreCommandHandler : IRequestHandler<UpdateAnswerScoreCommand, bool>
    {
        private readonly GenaricRepository<Domain.Entities.Answer> _answerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAnswerScoreCommandHandler(
            GenaricRepository<Domain.Entities.Answer> answerRepository,
            IUnitOfWork unitOfWork)
        {
            _answerRepository = answerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateAnswerScoreCommand request, CancellationToken cancellationToken)
        {
            var answer = await _answerRepository.GetByIdAsync(request.AnswerId);
            if (answer == null)
            {
                return false;
            }

            answer.Score = request.Score;
          await  _answerRepository.UpdateAsync(answer);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}

