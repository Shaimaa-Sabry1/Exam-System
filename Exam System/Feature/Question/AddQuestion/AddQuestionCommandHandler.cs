using Exam_System.Domain.Entities;
using Exam_System.Domain.Enums;
using Exam_System.Feature.Question.AddQuestion.Dtos;
using Exam_System.Feature.Questions.AddQuestions;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.AddQuestion
{
    public class AddQuestionCommandHandler(IQuestionRepository _questionRepository, ExamDbContext _dbContext, IConfiguration _configuration, IImageHelper _imageHelper) : IRequestHandler<AddQuestionCommand, AddQuestionToReturnDto>
    {
        public async Task<AddQuestionToReturnDto> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            var choices =await Task.WhenAll( request.Choices.Select(async c => new Choice
            {
                Text = c.Text,
                ImageURL =c.Image != null? $"{_configuration["BaseUrl"]}/{await _imageHelper.UploadImageAsync(c.Image, "Question-images")}" : null, 
                IsCorrect = c.IsCorrect,
            }));

            var question = new Domain.Entities.Question
            {
                Title = request.Title,
                Type = Enum.Parse<QuestionType>(request.Type.Replace(" ", "_"), ignoreCase: true),
                ExamId = request.ExamId,
                Choices = choices.ToList(),
            };
            await _questionRepository.AddAsync(question);
            var IsSaved = await _dbContext.SaveChangesAsync();

            //if (IsSaved == 0)
            //    throw new Exception($"Error While adding Question");

            var questionDto = new AddQuestionToReturnDto()
            {
                Id = question.Id,
                Title = question.Title,
                Type = question.Type.ToString().Replace("_", " "),
                ExamId = question.ExamId,
                Choices = question.Choices.Select(c => new ChoiceToReturnDto
                {
                    Text = c.Text,
                    ImageURL = c.ImageURL,
                    IsCorrect = c.IsCorrect
                }).ToList()
            };
            return questionDto;

        }

    }
}
