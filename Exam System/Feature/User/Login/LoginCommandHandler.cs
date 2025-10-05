using Exam_System.Infrastructure.Persistance.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userName", user.UserName),
                new Claim(ClaimTypes.Role, user.Role?.ToString()?? "student" )

            };

            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = user.UserName,
                Email = user.Email,
                Expiration = token.ValidTo
            };
        }
    }
}
