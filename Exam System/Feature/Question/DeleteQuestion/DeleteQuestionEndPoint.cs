using Exam_System.Feature.Question.DeleteQuestion;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Question.DeleteQuestion
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController(IMediator _mediator) : ControllerBase
    {
        [HttpDelete]
        public async Task<ActionResult<ResponseResult<bool>>> Delete(int QuestionId)
        {
            var isDeleted = await _mediator.Send(new DeleteQuestionCommand(QuestionId));
            return isDeleted.Success ? Ok(isDeleted) : NotFound(isDeleted);
        }
    }
}
