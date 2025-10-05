using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Exam.Queries.GetAllActiveExam
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController: ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveExams([FromQuery] GetAllActiveQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
