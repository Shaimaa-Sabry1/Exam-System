using Exam_System.Feature.StartExam.CreateAttembt.Command;
using Exam_System.Feature.StartExam.GetAttembt.Query;
using MediatR;
using Org.BouncyCastle.Bcpg;

namespace Exam_System.Feature.StartExam.Orchestrator.StartExam
{
    public record StartExamCommand(int examId , int userId ) : IRequest<StartExamResponseDto>;

    public class StartExamCommandHandler : IRequestHandler<StartExamCommand, StartExamResponseDto>
    {
        private readonly IMediator _mediator;

        public StartExamCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<StartExamResponseDto> Handle(StartExamCommand request, CancellationToken cancellationToken)
        {
            var attembtId = await _mediator.Send(new CreateAttembtCommand(request.examId, request.userId));
            var attembt = await _mediator.Send(new GetAttembtByIdQuery(attembtId));
            return new StartExamResponseDto
            {
                AttembtId = attembt.attembtId,
                Tiltle = attembt.Tiltle,
                DurationInMinutes = attembt.DurationInMinutes,
                QuestionsCount = attembt.QuestionsCount,
                Questions = attembt.Questions
            };
        }
    }

}
