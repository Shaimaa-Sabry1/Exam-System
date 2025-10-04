using Azure;
using Exam_System.Feature.User.ForgetPassword;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Exam_System.Feature.User.ResetPassword
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IMediator mediator, IMemoryCache memoryCache,IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ResponseResult<string>>> ResetPassword(ResetPasswordCommand resetedPassword)
        {
            if (_memoryCache.Get<ForgetPasswordResponse>(resetedPassword.PasswordToken) is not null)
            {
                var response = await _mediator.Send(resetedPassword);
                _memoryCache.Remove(resetedPassword.PasswordToken);
                await _unitOfWork.SaveChangesAsync();
                return response.Success ? Ok(response) : NotFound(response);
            }
            return BadRequest(ResponseResult<string>.FailResponse("token removed from cash"));
        }
    }
}
