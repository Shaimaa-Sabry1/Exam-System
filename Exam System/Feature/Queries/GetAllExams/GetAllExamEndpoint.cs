using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Queries.GetAllExams
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllExams([FromQuery] GetAllExamQuery query, [FromServices] IMediator mediator)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
