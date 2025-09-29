using Exam_System.Domain.Entities;

namespace Exam_System.Feature.User.RegisterUser
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword{ get; set; }
        public string PhoneNumber { get; set; }
    }
}
