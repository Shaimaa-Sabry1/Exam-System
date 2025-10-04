using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.User.CreateUserToken
{
    public class CreateUserTokenHandler : IRequestHandler<CreateUserTokenCommand>
    {
        private readonly ExamDbContext _examDbContext;

        public CreateUserTokenHandler(ExamDbContext examDbContext)
        {
            this._examDbContext = examDbContext;
        }
        public async Task<Unit> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var userToken = await _examDbContext.UserTokens.FirstOrDefaultAsync(r => r.UserId == request.UserId);
            if (userToken == null)
            {
                await _examDbContext.UserTokens.AddAsync(new UserToken
                {
                    UserId = request.UserId,
                    Token = request.Token,
                    CreatedAt = DateTime.UtcNow,
                    ExpiredDate = DateTime.UtcNow.AddMinutes(1),
                });
            }
            else
            {
                userToken.Token = request.Token;
                userToken.CreatedAt = DateTime.UtcNow;
                userToken.ExpiredDate = DateTime.UtcNow.AddMinutes(1);
            }
            return Unit.Value;
        }
    }
}
