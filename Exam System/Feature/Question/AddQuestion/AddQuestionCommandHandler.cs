using Exam_System.Domain.Entities;
using Exam_System.Domain.Exception;
using Exam_System.Feature.Question.AddQuestion;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;
using System.Linq.Expressions;

namespace Exam_System.Feature.Questions.AddQuestions
{
    public class AddQuestionCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<AddQuestionCommand, ResponseResult<AddQuestionToReturnDto>>
    {
        public async Task<ResponseResult<AddQuestionToReturnDto>> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exam = await _unitOfWork.Exam.GetByIdAsync(request.ExamId);
                if (exam == null)
                    return ResponseResult<AddQuestionToReturnDto>.FailResponse($"Exam with Id = {request.ExamId} Not Found");
                    //throw new ExamNotFoundException(request.ExamId);

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
                await _unitOfWork.Question.AddAsync(question);
                var IsSaved = await _unitOfWork.SaveChangesAsync();

                if (IsSaved == 0)
                    return ResponseResult<AddQuestionToReturnDto>.FailResponse($"Failed to add the question. Please try again.");

                var questionDto = new AddQuestionToReturnDto()
                {
                     Id=question.Id,
                      Title = question.Title,
                      Type = question.Type,
                      ExamId = question.ExamId,
                       Choices= question.Choices.Select(c => new ChoiceDto
                       {
                           Text = c.Text,
                           ImageURL = c.ImageURL,
                           IsCorrect = c.IsCorrect
                       }).ToList()
                };
                return ResponseResult<AddQuestionToReturnDto>.SuccessResponse(questionDto,$"Question Added Successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While adding Question : {ex.Message}", ex);
            }
        }
    }
}

