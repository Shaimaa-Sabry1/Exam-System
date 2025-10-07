using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.StartExam.Orchestrator.StartExam
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
        [HttpPost("StartExam")]
        public async Task<ActionResult<StartExamResponseDto>> StartExam([FromBody] StartExamCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
