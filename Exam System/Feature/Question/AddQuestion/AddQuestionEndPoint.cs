using Exam_System.Feature.Question.AddQuestion.Dtos;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Questions.AddQuestions
{
    [ApiController]
    [Route("api/[controller]")]

    public class QuestionController(IAddQuestionOrchestrator _addQuestionOrchestrator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ResponseResult<AddQuestionToReturnDto>>> AddQuestion([FromBody] AddQuestionRequestDto QuestionDto)
        {
            var result = await _addQuestionOrchestrator.AddAsync(QuestionDto.Title, QuestionDto.Type, QuestionDto.ExamId, QuestionDto.Choices);
            return Ok(result);
        }
    }
}