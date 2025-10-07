namespace Exam_System.Feature.Answer.SubmitExam
{
    public class AnswerDetailSubmissionDto
    {
        public int QuestionId { get; set; }
        public List<int> SelectedChoiceIds { get; set; } = new List<int>();
    }

    public class SubmitExamResponseDto
    {
        public bool IsSuccess { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsAutoSubmitted { get; set; } = false;
        public string? Message { get; set; }
        public List<WrongAnswerDetailDto> WrongAnswerDetails { get; set; } = new List<WrongAnswerDetailDto>();
    }

    public class WrongAnswerDetailDto
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; } = string.Empty;
        public List<int> SelectedChoiceIds { get; set; } = new List<int>();
        public List<int> CorrectChoiceIds { get; set; } = new List<int>();
        public List<ChoiceDetailDto> SelectedChoices { get; set; } = new List<ChoiceDetailDto>();
        public List<ChoiceDetailDto> CorrectChoices { get; set; } = new List<ChoiceDetailDto>();
    }

    public class ChoiceDetailDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}

