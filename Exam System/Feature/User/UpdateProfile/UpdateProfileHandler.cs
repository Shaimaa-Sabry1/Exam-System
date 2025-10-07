using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.User.UpdateProfile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, ResponseResult<string>>
    {
        private readonly ExamDbContext _dbContext;
        private readonly IImageHelper _helper;

        public UpdateProfileHandler(ExamDbContext dbContext,IImageHelper helper)
        {
            this._dbContext = dbContext;
            this._helper = helper;
        }
        public async Task<ResponseResult<string>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.Id);
            if (user == null)
            {
                return ResponseResult<string>.FailResponse("User not found");
            }
            bool isUpdated = false;
            if (!string.IsNullOrEmpty(request.FirstName) && user.FirstName != request.FirstName)
            {
                user.FirstName = request.FirstName;
                isUpdated = true;
            }
            if (!string.IsNullOrEmpty(request.LastName) && user.LastName != request.LastName)
            {
                user.LastName = request.LastName;
                isUpdated = true;
            }
            if(!string.IsNullOrEmpty(request.UserName)&& user.UserName!=request.UserName)
            {
                user.UserName = request.UserName;
                isUpdated = true;
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber) && user.PhoneNumber != request.PhoneNumber)
            {
                user.PhoneNumber = request.PhoneNumber;
                isUpdated = true;
            }
            if(request.ImageUrl != null)
            {
                var imageUrl = string.IsNullOrEmpty(user.ProfileImageUrl)? await _helper.UploadImageAsync(request.ImageUrl, "uploads/profile"):
                    await _helper.UpdateImageAsync(request.ImageUrl, user.ProfileImageUrl, "uploads/profile")
                    ;
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    user.ProfileImageUrl = imageUrl;
                    isUpdated = true;
                }
            }
            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                if(user.Password!=request.OldPassword)
                {
                    return ResponseResult<string>.FailResponse("Old password is incorrect");
                }
                user.Password = request.NewPassword;
                isUpdated = true;


            }
            if(!isUpdated)
            {
                return ResponseResult<string>.FailResponse("No changes detected");
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return ResponseResult<string>.SuccessResponse("Profile updated successfully");
        }
    }
}
