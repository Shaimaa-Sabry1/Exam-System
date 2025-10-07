using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Answer.AnswerDetail
{
    [ApiController]
    [Route("api/[controller]/CreateDetailAnswer")]
    public class AnswerDetailController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateAnswerDetail([FromBody] CreateAnswerDetailsCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}
