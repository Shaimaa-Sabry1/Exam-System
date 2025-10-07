using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.User.UpdateProfile
{
    public class UpdateProfileCommand: IRequest<ResponseResult<string>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
