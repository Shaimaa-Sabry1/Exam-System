using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Categories.DeleteCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController: ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            
            var result = await _mediator.Send(new DeleteCategoryCommand { CategoryId = id });

            if(!result) return NotFound();
            return Ok(result);
        }
    }
}
