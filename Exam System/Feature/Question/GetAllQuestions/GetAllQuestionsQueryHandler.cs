using Exam_System.Domain.Entities;
using Exam_System.Domain.Exception;
using Exam_System.Feature.Question.AddQuestion.Dtos;
using Exam_System.Feature.Question.GetAllQuestions.Dtos;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using Exam_System.Shared.Specification;
using MediatR;

namespace Exam_System.Feature.Question.GetAllQuestions
{
    public class GetAllQuestionsQueryHandler(IQuestionRepository _questionRepository) : IRequestHandler<GetAllQuestionsQuery, ResponseResult<GetAllQuestionsResponseDto>>
    {
        public async Task<ResponseResult<GetAllQuestionsResponseDto>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Question> questions = Enumerable.Empty<Domain.Entities.Question>();
            int totalCount = 0;

            if (request.ExamId.HasValue && request.ExamId.Value > 0)
            {           
                (questions, totalCount) = await _questionRepository.GetAllQuestionsAsync(new QuestionsByExamIdSpecification(request.ExamId.Value));
            }

            else if (!string.IsNullOrEmpty(request.QuestionName))
            {
                (questions, totalCount) = await _questionRepository.GetAllQuestionsAsync(new QuestionsByQuestionNameSpecification(request.QuestionName));
            }
            else
            {
                return ResponseResult<GetAllQuestionsResponseDto>.FailResponse("Please provide ExamId or QuestionName.");
            }

            if (!questions.Any())
                return ResponseResult<GetAllQuestionsResponseDto>.FailResponse("No questions found.");

            var questionsDto = questions.Select(Q => new GetAllQuestionsDto
            {
                Title = Q.Title,
                Type = Q.Type.ToString().Replace("_"," "),
                Choices = Q.Choices.Select(C => new ChoiceToReturnDto
                {
                    Text = C.Text,
                    ImageURL = C.ImageURL,
                    IsCorrect = C.IsCorrect
                }).ToList()
            }).ToList();

            var response = new GetAllQuestionsResponseDto
            {
                Questions = questionsDto,
                TotalCount = totalCount
            };

            return ResponseResult<GetAllQuestionsResponseDto>.SuccessResponse(response, "Questions retrieved successfully.");
        }
    }
}
