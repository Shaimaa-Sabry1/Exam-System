using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Exam.DeleteExam
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
        [HttpDelete("{Id}")]

        public async Task<IActionResult> DeleteExamById(int Id)
        {
            var result = await _mediator.Send(new DeleteExamCommand { Id = Id });
            if (!result) return NotFound();
            return Ok(result);
        }

    }
}
