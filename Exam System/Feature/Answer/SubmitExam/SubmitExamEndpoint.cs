using Exam_System.Feature.Answer.SubmitExam.Orchestrator.SubmitExam;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Answer.SubmitExam
{
    [ApiController]
    [Route("api/[controller]/Submit")]
    public class ExamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SubmitExam")]
        public async Task<IActionResult> SubmitExam([FromBody] SubmitExamCommand command)
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

