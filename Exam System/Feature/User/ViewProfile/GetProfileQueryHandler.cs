using Exam_System.Infrastructure.Persistance.Data;
using MediatR;
using System.Security.Claims;

namespace Exam_System.Feature.User.ViewProfile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ViewProfileDto>
    {
        private readonly ExamDbContext _dbContext;
        private readonly IHttpContextAccessor _accessor;

        public GetProfileQueryHandler(ExamDbContext dbContext,IHttpContextAccessor accessor )
        {
            this._dbContext = dbContext;
            this._accessor = accessor;
        }
        public async Task<ViewProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var user = await _dbContext.Users.FindAsync(int.Parse(userId));
            if(user == null)
            {
                return null;
            }
            return new ViewProfileDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName=user.UserName,
                Email = user.Email,
                ImageUrl = user.ProfileImageUrl,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString()
            };
        }
    }
}
