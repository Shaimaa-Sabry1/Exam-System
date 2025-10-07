using Exam_System.Feature.Answer.SubmitExam.Orchestrator.SubmitExam;
using FluentValidation;

namespace Exam_System.Feature.Answer.SubmitExam
{
    public class SubmitExamValidator : AbstractValidator<SubmitExamCommand>
    {
        public SubmitExamValidator()
        {
            RuleFor(x => x.AttemptId)
                .GreaterThan(0)
                .WithMessage("Attempt ID must be valid");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User ID must be valid");

            RuleFor(x => x.AnswerDetails)
                .NotNull()
                .WithMessage("Answer details cannot be null");

            RuleForEach(x => x.AnswerDetails)
                .ChildRules(answerDetail =>
                {
                    answerDetail.RuleFor(ad => ad.QuestionId)
                        .GreaterThan(0)
                        .WithMessage("Question ID must be valid");

                    answerDetail.RuleFor(ad => ad.SelectedChoiceIds)
                        .NotNull()
                        .WithMessage("Selected choice IDs cannot be null");
                });
        }
    }
}

