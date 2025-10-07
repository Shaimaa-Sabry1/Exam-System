using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Answer.AnswerDetail.Delete
{
    [ApiController]
    [Route("api/[controller]/Delete")]
    public class AnswerDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswerDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{answerDetailId}")]
        public async Task<IActionResult> DeleteAnswerDetail(int answerDetailId)
        {
            var result = await _mediator.Send(new DeleteAnswerDetailCommand(answerDetailId));
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}

