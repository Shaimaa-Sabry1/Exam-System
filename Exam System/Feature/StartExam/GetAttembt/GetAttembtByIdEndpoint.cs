using Exam_System.Feature.StartExam.GetAttembt.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.StartExam.GetAttembt
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
        [HttpGet]
        public async Task<IActionResult> GetAttembt([FromQuery] GetAttembtByIdQuery query, [FromServices] IMediator mediator)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
