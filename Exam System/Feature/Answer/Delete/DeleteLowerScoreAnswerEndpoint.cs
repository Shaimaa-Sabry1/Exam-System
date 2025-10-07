using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Answer.Delete
{
    [ApiController] 
    [Route("api/[controller]/DeleteLowerScore")]
    public class DeleteLowerScoreAnswerController : ControllerBase
    {
        private readonly IMediator _mediatorInstance;

        public DeleteLowerScoreAnswerController(IMediator mediator)
        {
            _mediatorInstance = mediator;
        }

        [HttpDelete("DeleteLowerScoreAnswers/{newAnswerId}")]
        public async Task<IActionResult> DeleteLowerScoreAnswers(int newAnswerId)
        {
            var result = await _mediatorInstance.Send(new DeleteLowerScoreAnswerCommand(newAnswerId));
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}

