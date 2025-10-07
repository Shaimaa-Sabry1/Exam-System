using Exam_System.Domain.Exception;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Answer.Delete
{
    public record DeleteAnswerCommand(int AnswerId) : IRequest<ResponseResult<bool>>;

    public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand, ResponseResult<bool>>
    {
        private readonly GenaricRepository<Domain.Entities.Answer> _answerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAnswerCommandHandler(
            GenaricRepository<Domain.Entities.Answer> answerRepository,
            IUnitOfWork unitOfWork)
        {
            _answerRepository = answerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResult<bool>> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _answerRepository.GetByIdAsync(request.AnswerId);
            if (answer == null)
            {
                return ResponseResult<bool>.FailResponse($"Answer with Id {request.AnswerId} not found");
            }

            await _answerRepository.DeleteAsync(answer);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == 0)
            {
                return ResponseResult<bool>.FailResponse("Failed to delete answer");
            }

            return ResponseResult<bool>.SuccessResponse(true, "Answer deleted successfully");
        }
    }
}

