using System.Security.Claims;
using Exam_System.Feature.StartExam.Command;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.StartExam.StartExamEndPoint
{
    [Route("api/[controller]")]
    [ApiController]

    public class ExamController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExamController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("StartExam")]
        public async Task<IActionResult> StartExam([FromBody] StartExamCommand command)
        {
            
        
            var result = await _mediator.Send(command);

            
            return Ok(result);
        }


    }
}
