using Exam_System.Domain.Entities;
using Exam_System.Feature.Question.GetAllQuestions.Dtos;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam_System.Feature.Question.GetAllQuestions
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseResult<GetAllQuestionsResponseDto>>> GetAll([FromQuery] int? examId, [FromQuery] string? questionName)
        {
            var result = await _mediator.Send(new GetAllQuestionsQuery(examId, questionName));
            return Ok(result);
        }
       
    }
}
