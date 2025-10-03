using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Exam.UpdateExam
{
    [ApiController]
    [Route("api/[controller]")]

    public class ExamController  :ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateExam([FromRoute] int Id,[FromBody] UpdateExamCommand command)
        {   
            command.Id=Id;
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
