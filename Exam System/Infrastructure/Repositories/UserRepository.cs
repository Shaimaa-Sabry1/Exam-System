using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Exam_System.Infrastructure.Repositories
{
    public class UserRepository : GenaricRepository<User>, IUserRepository
    {
        public UserRepository(ExamDbContext dbcontext) : base(dbcontext)
        {
        }
        public override async Task<User> AddAsync(User user)
        {
            string password = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(user.Password));
            user.Password = password;
            return await base.AddAsync(user);
        }
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.Verify(password, user.Password));

        }
        public async Task<(bool, User?)> CheckUserExistAsync(IFilterSpecification<User> specification)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(specification.Criteria);
            return (user is not null, user);
        }

        public async Task AddClaimsAsync(List<UserClaim> Claims)
        {
            await _dbcontext.UserClaims.AddRangeAsync(Claims);
        }

        public async Task<List<Claim>> GetClaimsAsync(User user)
        {
            return await _dbcontext.UserClaims.Where(u => u.UserId == user.Id).Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToListAsync();
        }
    }
}
