using Exam_System.Feature.Question.EditQuestion.Dtos;
using Exam_System.Feature.Questions.AddQuestions;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Questions.EditQuestion
{
    [ApiController]
    [Route("api/[controller]")]


    public class QuestionController(IMediator _mediator) : ControllerBase
    {
        [HttpPut]
        public async Task<ActionResult<ResponseResult<EditQuestionToReturnDto>>> EditQuestion([FromBody] EditQuestionRequestDto QuestionDto)
        {
            var result = await _mediator.Send(new EditQuestionCommand(QuestionDto.Id, QuestionDto.Title, QuestionDto.Type, QuestionDto.Choices));
            return Ok(result);
        }
    }
}