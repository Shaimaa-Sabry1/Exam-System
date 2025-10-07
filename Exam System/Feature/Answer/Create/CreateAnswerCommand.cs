using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Exam_System.Feature.Answer.Create
{
    public record CreateAnswerCommand (int UserId,int AttembtId) : IRequest<int>;

    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, int>
    {
        private readonly GenaricRepository<Domain.Entities.Answer> _answerRebository;
        private readonly IDistributedCache _distributedCache;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAnswerCommandHandler(
            GenaricRepository<Domain.Entities.Answer> AnswerRebository,
            IDistributedCache distributedCache,
            IUnitOfWork unitOfWork
            )
        {
            _answerRebository = AnswerRebository;
            _distributedCache = distributedCache;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {

            var answerObject = new Domain.Entities.Answer
            {
                UserId = request.UserId,
                attembtId = request.AttembtId,
                SubmittedAt = DateTime.Now,
                Score = 0
            };
            var answer =  await _answerRebository.AddAsync(answerObject);
            await _unitOfWork.SaveChangesAsync();
            
           var ChacheKey = $"Answer_{answer.Id}";
            var ChacheValue = _distributedCache.GetString(ChacheKey);
            if (ChacheValue == null)
            {
                var JsonAnswer = System.Text.Json.JsonSerializer.Serialize(answer);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30), // expire after 30 min
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };
                _distributedCache.SetString(ChacheKey, JsonAnswer, options);
            }



            return answer.Id;

        }
    }

}
