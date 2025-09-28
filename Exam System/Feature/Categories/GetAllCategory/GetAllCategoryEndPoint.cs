using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Categories.GetAllCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController:ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoryQuery query,[FromServices] IMediator mediator)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
