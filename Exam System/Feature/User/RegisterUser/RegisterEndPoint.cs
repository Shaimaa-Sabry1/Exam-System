using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.User.RegisterUser
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpPost]
        public ActionResult Register(RegisterDTO regiserDTO) { 
            return Ok();
        }
    }
}
