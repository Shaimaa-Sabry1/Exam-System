using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Exams.Endpoint
{
    [ApiController]
    [Route("api/exam")]
    public class ExamEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult>CreateExam([FromBody] Commands.CreateExamCommand command)
        {
            var examId = await _mediator.Send(command);
            return Ok(examId);
        }
    }
}
