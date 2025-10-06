using Exam_System.Domain.Entities;
using Exam_System.Domain.Exception;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.GetAllQuestions
{
    public class GetAllQuestionsQueryHandler(IQuestionRepository _questionRepository) : IRequestHandler<GetAllQuestionsQuery, ResponseResult<GetAllQuestionsResponse>>
    {
        public async Task<ResponseResult<GetAllQuestionsResponse>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Question> questions = Enumerable.Empty<Domain.Entities.Question>();
            int totalCount = 0;

            if (request.ExamId.HasValue && request.ExamId.Value > 0)
            {           
                (questions, totalCount) = await _questionRepository.GetAllQuestionsAsync(request.ExamId.Value);
            }

            else if (!string.IsNullOrEmpty(request.QuestionName))
            {
                (questions, totalCount) = await _questionRepository.GetAllQuestionsAsync(request.QuestionName);
            }
            else
            {
                return ResponseResult<GetAllQuestionsResponse>.FailResponse("Please provide ExamId or QuestionName.");
            }

            if (!questions.Any())
                return ResponseResult<GetAllQuestionsResponse>.FailResponse("No questions found.");

            var questionsDto = questions.Select(Q => new GettAllQuestionsDto
            {
                Title = Q.Title,
                Type = Q.Type,
                Choices = Q.Choices.Select(C => new Domain.Entities.ChoiceDto
                {
                    Text = C.Text,
                    ImageURL = C.ImageURL,
                    IsCorrect = C.IsCorrect
                }).ToList()
            });

            var response = new GetAllQuestionsResponse
            {
                Questions = questionsDto,
                TotalCount = totalCount
            };

            return ResponseResult<GetAllQuestionsResponse>.SuccessResponse(response, "Questions retrieved successfully.");
        }
    }
}
