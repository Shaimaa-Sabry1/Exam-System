using Exam_System.Feature.Question.DeleteQuestion;
using Exam_System.Feature.Question.ListQuestionsTypes.Dtos;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Question.ListQuestionsTypes
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("QuestionTypes")]
        public async Task<ActionResult<ResponseResult<List<QuestionTypeDto>>>> GetQuestionTypes()
        {
            var result = await _mediator.Send(new QuestionTypeQuery());
            return Ok(result);
        }
    }
}
