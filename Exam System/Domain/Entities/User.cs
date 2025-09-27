namespace Exam_System.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public string? ProfileImageUrl { get; set; }

        public ICollection<UserClaim> Claims { get; set; } = new HashSet<UserClaim>();

    }
}
