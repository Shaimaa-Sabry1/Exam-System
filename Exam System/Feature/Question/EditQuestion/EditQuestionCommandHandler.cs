using Exam_System.Domain.Entities;
using Exam_System.Domain.Exception;
using Exam_System.Feature.Question.AddQuestion;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Questions.EditQuestion
{
    public class EditQuestionCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<EditQuestionCommand, ResponseResult<EditQuestionToReturnDto>>
    {
        public async Task<ResponseResult<EditQuestionToReturnDto>> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            var Question = await _unitOfWork.Question.GetByIdAsync(request.QuestionId);
            if (Question == null)
                return ResponseResult<EditQuestionToReturnDto>.FailResponse($"Question with Id = {request.QuestionId} Not Found");
                //throw new QuestionNotFoundException(request.QuestionId);
            Question.Title = request.Title;
            Question.Type = request.Type;
            //Question.Choices=request.Choices;
            if (request.Choices != null)
            {
                Question.Choices.Clear();
                foreach (var choice in request.Choices)
                {
                    Question.Choices.Add(new Choice()
                    {
                        Text = choice.Text,
                        ImageURL = choice.ImageURL,
                        IsCorrect = choice.IsCorrect,
                        QuestionId = Question.Id,
                    });
                }
            }
            await _unitOfWork.Question.UpdateAsync(Question);
            var IsSaved = await _unitOfWork.SaveChangesAsync();
            if (IsSaved == 0)
                return ResponseResult<EditQuestionToReturnDto>.FailResponse($"Failed to Edit the question. Please try again.");

            //mapper
            var QuestionDto = new EditQuestionToReturnDto()
            {
                Title = Question.Title,
                Type = Question.Type,
                ExamId = Question.ExamId,
                Choices = Question.Choices.Select(c => new ChoiceDto()
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
