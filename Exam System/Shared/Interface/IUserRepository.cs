using Exam_System.Domain.Entities;
using System.Security.Claims;

namespace Exam_System.Shared.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<(bool Exists, User? User)> CheckUserExistAsync(IFilterSpecification<User> specification);
        Task AddClaimsAsync(List<UserClaim> Claims);
        Task<List<Claim>> GetClaimsAsync(User user);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
