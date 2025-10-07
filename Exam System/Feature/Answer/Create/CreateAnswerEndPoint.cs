using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Answer.Create
{
    [ApiController]
    [Route("api/[controller]/CreateAnswer")]
    public class AnswerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerCommand command)
        {
            var answerId = await _mediator.Send(command);
            return Ok(answerId);
        }
    }
}
