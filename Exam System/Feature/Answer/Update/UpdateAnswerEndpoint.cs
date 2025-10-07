using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Answer.Update
{
    [ApiController]
    [Route("api/[controller]/Update")]
    public class AnswerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{answerId}")]
        public async Task<IActionResult> UpdateAnswer(int answerId, [FromBody] UpdateAnswerCommand command)
        {
            // Ensure the AnswerId in the command matches the route parameter
            var updatedCommand = command with { AnswerId = answerId };
            var result = await _mediator.Send(updatedCommand);
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}

