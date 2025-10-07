using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Answer.Delete
{
    [ApiController]
    [Route("api/[controller]/Delete")]
    public class AnswerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{answerId}")]
        public async Task<IActionResult> DeleteAnswer(int answerId)
        {
            var result = await _mediator.Send(new DeleteAnswerCommand(answerId));
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}

