using MediatR;

namespace Exam_System.Feature.User.Login
{
    public class LoginCommand:IRequest<LoginResponseDto>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
