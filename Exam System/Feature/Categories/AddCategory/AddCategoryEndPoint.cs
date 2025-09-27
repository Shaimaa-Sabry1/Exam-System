using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Categories.AddCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public  class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            return Ok(categoryId);
        }
    }
}
