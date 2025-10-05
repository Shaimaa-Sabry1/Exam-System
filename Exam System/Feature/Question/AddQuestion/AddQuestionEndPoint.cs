using Exam_System.Domain.Entities;
using Exam_System.Feature.Question.AddQuestion;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Questions.AddQuestions
{
    [ApiController]
    [Route("api/[controller]")]

    public class QuestionController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ResponseResult<AddQuestionToReturnDto>>> AddQuestion([FromBody] AddQuestionRequestDto QuestionDto)
        {
            var result = await _mediator.Send(new AddQuestionCommand(QuestionDto.Title, QuestionDto.Type, QuestionDto.ExamId, QuestionDto.Choices));
            return Ok(result);
        }
    }
}