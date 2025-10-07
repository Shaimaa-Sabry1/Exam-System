using Exam_System.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin")]
    public class DashboardController:ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet("report")]
        public async Task<IActionResult> GetDashboardReport()
        {
            var query = new GetDashboardReportQuery();
            var result = await _mediator.Send(query);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
