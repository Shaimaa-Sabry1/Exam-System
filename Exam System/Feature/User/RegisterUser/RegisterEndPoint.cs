using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exam_System.Feature.User.RegisterUser
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseResult<string>>> Register([FromBody] RegisterDTO regiserDTO)
        {
            var response = await _mediator.Send(new RegisterCommand(
                 regiserDTO.FirstName,
                 regiserDTO.LastName,
                 regiserDTO.UserName,
                 regiserDTO.Email,
                 regiserDTO.Password,
                 regiserDTO.ConfirmPassword,
                 regiserDTO.PhoneNumber
             ));
            return response.Success ? Ok(response) : BadRequest(response);

        }
    }
}
