using Exam_System.Feature.StartExam.CreateAttembt.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.StartExam.CreateAttembt.CreateAttembtEndPoint
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttembtController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AttembtController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> StartExam([FromBody]  Command.CreateAttembtCommand command )
        {
            var result = await _mediator.Send( command  );
            return Ok(result);
        }
    }
}
