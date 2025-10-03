using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Exam.Queries.GetAllExams
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExams([FromQuery] GetAllExamQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
