using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.User.ViewProfile
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet("ViewProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var rsult = await _mediator.Send(new GetProfileQuery());
            if (rsult != null)
            {
                return Unauthorized(new {Message= " Unauthorized: You must be logged in." });
            }
            return Ok();
        }
    }
}
