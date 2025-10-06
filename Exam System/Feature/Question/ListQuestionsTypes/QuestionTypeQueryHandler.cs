using Exam_System.Domain.Enums;
using Exam_System.Feature.Question.DeleteQuestion;
using Exam_System.Feature.Question.ListQuestionsTypes.Dtos;
using Exam_System.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Feature.Question.ListQuestionsTypes
{
    public class QuestionTypeQueryHandler : IRequestHandler<QuestionTypeQuery, ResponseResult<List<QuestionTypeDto>>>
    {
       
        public Task<ResponseResult<List<QuestionTypeDto>>> Handle(QuestionTypeQuery request, CancellationToken cancellationToken)
        {
            var QuestionTypes = Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>()
                           .Select(t => new QuestionTypeDto
                           {
                               Id = (int)t,
                               Name = t.ToString().Replace("_", " ")
                           })
                           .ToList();
            if(QuestionTypes.Count == 0)
            {
                return Task.FromResult(ResponseResult<List<QuestionTypeDto>>.FailResponse("No Question Types Found"));
            }
            return Task.FromResult(ResponseResult<List<QuestionTypeDto>>.SuccessResponse(QuestionTypes));
                        
        }
    }
}
