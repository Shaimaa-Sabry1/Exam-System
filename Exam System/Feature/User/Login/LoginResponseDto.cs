namespace Exam_System.Feature.User.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Expiration { get; set; }
    }
}
