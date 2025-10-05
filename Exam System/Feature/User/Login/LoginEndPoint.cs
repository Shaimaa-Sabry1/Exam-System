using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.User.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginEndPoint(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return Unauthorized(new { Message = "Invalid username or password" });
            return Ok(result);
        }
    }
}
