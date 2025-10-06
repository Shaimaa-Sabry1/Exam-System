using Exam_System.Domain.Entities;
using Exam_System.Domain.Enums;
using Exam_System.Domain.Exception;
using Exam_System.Feature.Question.AddQuestion;
using Exam_System.Feature.Question.AddQuestion.Dtos;
using Exam_System.Feature.Question.EditQuestion.Dtos;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Questions.EditQuestion
{
    public class EditQuestionCommandHandler(IQuestionRepository _questionRepository , ExamDbContext _dbContext , IConfiguration _configuration , IImageHelper _imageHelper) : IRequestHandler<EditQuestionCommand, ResponseResult<EditQuestionToReturnDto>>
    {
        public async Task<ResponseResult<EditQuestionToReturnDto>> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            var Question = await _questionRepository.GetByIdAsync(request.QuestionId);
            if (Question == null)
                return ResponseResult<EditQuestionToReturnDto>.FailResponse($"Question with Id = {request.QuestionId} Not Found");
            Question.Title = request.Title;
            Question.Type = Enum.Parse<QuestionType>(request.Type.Replace(" ","_"),ignoreCase:true);
            if (request.Choices != null)
            {
                Question.Choices.Clear();
                foreach (var choice in request.Choices)
                {
                    Question.Choices.Add(new Choice()
                    {
                        Text = choice.Text,
                        ImageURL = choice.Image != null ? $"{_configuration["BaseUrl"]}/{await _imageHelper.UploadImageAsync(choice.Image, "Question-images")}":null,
                        IsCorrect = choice.IsCorrect,
                        QuestionId = Question.Id,
                    });
                }
            }
            await _questionRepository.UpdateAsync(Question);
            var IsSaved = await _dbContext.SaveChangesAsync();
            if (IsSaved == 0)
                return ResponseResult<EditQuestionToReturnDto>.FailResponse($"Failed to Edit the question. Please try again.");

            //mapper
            var QuestionDto = new EditQuestionToReturnDto()
            {
                Title = Question.Title,
                Type = Question.Type.ToString(),
                ExamId = Question.ExamId,
                Choices = Question.Choices.Select(c => new ChoiceToReturnDto()
                {
                    Text = c.Text,
                    ImageURL = c.ImageURL,
                    IsCorrect = c.IsCorrect,

                }).ToList()
            };

            return ResponseResult<EditQuestionToReturnDto>.SuccessResponse(QuestionDto,$"Question Edited Successfully");
        }
    }
}
