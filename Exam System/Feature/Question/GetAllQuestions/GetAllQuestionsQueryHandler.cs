using Exam_System.Domain.Entities;
using Exam_System.Domain.Exception;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.GetAllQuestions
{
    public class GetAllQuestionsQueryHandler(IQuestionRepository _questionRepository , IExamRepository _examRepository) : IRequestHandler<GetAllQuestionsQuery, ResponseResult<GetAllQuestionsResponse>>
    {
        public async Task<ResponseResult<GetAllQuestionsResponse>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            var exam =await _examRepository.GetByIdAsync(request.ExamId);
            if (exam == null)
                return ResponseResult<GetAllQuestionsResponse>.FailResponse($"Not Found This Exam with Id = {request.ExamId}");
            //throw new ExamNotFoundException(request.ExamId);

            var (Questions,TotalCount) =await _questionRepository.GetAllAsync(request.ExamId);
            if (!Questions.Any())
                return ResponseResult<GetAllQuestionsResponse>.FailResponse($"Not Found Questions!!!");

            var QuestionsDto = Questions.Select(Q => new GettAllQuestionsDto()
            {
                Title = Q.Title,
                Type = Q.Type,
                Choices = Q.Choices.Select(C => new Domain.Entities.ChoiceDto()
                {
                    Text = C.Text,
                    ImageURL = C.ImageURL,
                    IsCorrect = C.IsCorrect,
                }).ToList()
            });
            var QuestionsResponse = new GetAllQuestionsResponse()
            {
                Questions = QuestionsDto,
                TotalCount = TotalCount
            };
            return ResponseResult<GetAllQuestionsResponse>.SuccessResponse(QuestionsResponse,$"Questions Retrieved Successfully");
        }
    }
}
