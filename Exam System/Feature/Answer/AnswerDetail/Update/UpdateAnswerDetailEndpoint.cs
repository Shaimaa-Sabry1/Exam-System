using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Answer.AnswerDetail.Update
{
    [ApiController]
    [Route("api/[controller]/Update")]
    public class AnswerDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswerDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{answerDetailId}")]
        public async Task<IActionResult> UpdateAnswerDetail(int answerDetailId, [FromBody] UpdateAnswerDetailCommand command)
        {
            // Ensure the AnswerDetailId in the command matches the route parameter
            var updatedCommand = command with { AnswerDetailId = answerDetailId };
            var result = await _mediator.Send(updatedCommand);
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}

