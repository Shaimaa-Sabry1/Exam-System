using Exam_System.Feature.Question.AddQuestion.Dtos;
using Exam_System.Feature.Question.ListQuestionsTypes.Dtos;
using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Questions.AddQuestions
{
    public record AddQuestionCommand(string Title, string Type, int ExamId, List<ChoiceDto> Choices) : IRequest<AddQuestionToReturnDto>
    {
    }
}
