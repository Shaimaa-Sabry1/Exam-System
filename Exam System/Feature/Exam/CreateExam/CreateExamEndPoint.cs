using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Exams.Endpoint
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] Commands.CreateExamCommand command)
        {
             
            var examId = await _mediator.Send(command);
            return Ok(examId);
        }
    }
}