using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Categories.Endpoints
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesEndpoint: ControllerBase
    {
        private readonly IMediator _mediator;

       public CategoriesEndpoint(IMediator mediator)
        {
            this._mediator = mediator;
            
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Commands.AddCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            return Ok(categoryId);
        }
    }
}
