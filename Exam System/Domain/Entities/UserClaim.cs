
namespace Exam_System.Domain.Entities
{
    public class UserClaim
    {
        public UserClaim() { }
        public UserClaim(User user, string claimType, string claimValue)
        {
            User = user;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public int Id { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

    }
}
