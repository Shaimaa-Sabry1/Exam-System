using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Exam.UpdateExam
{
    [ApiController]
    [Route("api/[controller]")]

    public class ExamController  :ControllerBase
    {
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateExam([FromRoute] int Id,[FromBody] UpdateExamCommand command, [FromServices] IMediator mediator)
        {   
            command.Id=Id;
            var result = await mediator.Send(command);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
