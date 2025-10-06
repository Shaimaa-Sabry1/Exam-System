using Exam_System.Domain.Exception;
using Exam_System.Feature.Exam.Queries.GetExamById;
using Exam_System.Feature.Question.AddQuestion.Dtos;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using System.Linq.Expressions;

namespace Exam_System.Feature.Questions.AddQuestions
{
    public class AddQuestionOrchestrator(IMediator _mediator) : IAddQuestionOrchestrator
    {
        public async Task<ResponseResult<AddQuestionToReturnDto>> AddAsync(string Title, string Type, int ExamId, List<ChoiceDto> Choices)
        {

            var exam = await _mediator.Send(new GetExamByIdQuery(ExamId));
            if (exam == null)
                return ResponseResult<AddQuestionToReturnDto>.FailResponse($"Exam with Id = {ExamId} Not Found");

            var questionDto = await _mediator.Send(new AddQuestionCommand(Title,Type,ExamId,Choices));
           
            return ResponseResult<AddQuestionToReturnDto>.SuccessResponse(questionDto, $"Question Added Successfully");

        }
    }
}

