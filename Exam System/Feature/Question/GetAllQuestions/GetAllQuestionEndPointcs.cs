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
        public async Task<ActionResult<ResponseResult<GetAllQuestionsResponse>>> GetAll(int ExamId)
        {
            var questions =await _mediator.Send(new GetAllQuestionsQuery(ExamId));      

            return Ok(questions);
        }
    }
}
