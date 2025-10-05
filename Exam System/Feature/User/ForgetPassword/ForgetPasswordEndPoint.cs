using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Exam_System.Feature.User.ForgetPassword
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IMediator mediator, IMemoryCache memoryCache, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<ResponseResult<ForgetPasswordResponse>>> ForgetPassword(ForgetPasswordCommand emailUser)
        {
            var response = await _mediator.Send(emailUser);
            if (response.Success)
            {
                _memoryCache.Set(response.Data.TokenPassword, response.Data.Id);
                await _unitOfWork.SaveChangesAsync();
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
