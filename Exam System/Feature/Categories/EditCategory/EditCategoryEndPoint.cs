using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Categories.EditCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] EditCategoryCommand command)
        {
            if (id != command.Id) return BadRequest("Id in URL does not match Id in body");
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
