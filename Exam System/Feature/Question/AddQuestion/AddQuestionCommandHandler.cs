using Exam_System.Domain.Entities;
using Exam_System.Feature.Questions.AddQuestions;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Question.AddQuestion
{
    public class AddQuestionCommandHandler(IQuestionRepository _questionRepository, ExamDbContext _dbContext) : IRequestHandler<AddQuestionCommand, AddQuestionToReturnDto>
    {
        public async Task<AddQuestionToReturnDto> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {

            var question = new Domain.Entities.Question
            {
                Title = request.Title,
                Type = request.Type,
                ExamId = request.ExamId,
                Choices = request.Choices.Select(c => new Choice
                {
                    Text = c.Text,
                    ImageURL = c.ImageURL,
                    IsCorrect = c.IsCorrect
                }).ToList()
            };
            await _questionRepository.AddAsync(question);
            var IsSaved = await _dbContext.SaveChangesAsync();

            //if (IsSaved == 0)
                //throw new Exception($"Error While adding Question : {ex.Message}", ex);
            //return ResponseResult<AddQuestionToReturnDto>.FailResponse($"Failed to add the question. Please try again.");

            var questionDto = new AddQuestionToReturnDto()
            {
                Id = question.Id,
                Title = question.Title,
                Type = question.Type,
                ExamId = question.ExamId,
                Choices = question.Choices.Select(c => new ChoiceDto
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
