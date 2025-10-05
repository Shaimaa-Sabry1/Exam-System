using Exam_System.Infrastructure.Persistance.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace Exam_System.Feature.User.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IConfiguration _configuration;
        private readonly ExamDbContext _dbContext;

        public LoginCommandHandler(ExamDbContext dbContext,IConfiguration configuration)
        {
            this._configuration = configuration;
            this._dbContext = dbContext;
        }
        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user=await _dbContext.Users
                .FirstOrDefaultAsync(u=>
                u.UserName==request.UserNameOrEmail || u.Email==request.UserNameOrEmail,cancellationToken);
            if (user == null) return null;
            if(!BCrypt.Net.BCrypt.Verify(request.Password,user.Password))
                return null;
        }
    }
}
