using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.StartExam.Delete_attembt.endpoint
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttembtController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttembtController(IMediator mediator )
        {
            this._mediator = mediator;
        }
        [HttpDelete]
        public async Task<bool> DeleteAttembt([FromQuery] DeleteAttembtCommand command)
        {
            var result =  await _mediator.Send(command);
           
            return result;
        }

    }
}
