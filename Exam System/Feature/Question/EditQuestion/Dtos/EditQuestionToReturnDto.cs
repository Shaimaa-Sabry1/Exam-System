using Exam_System.Feature.Question.AddQuestion.Dtos;
using System.Reflection;

namespace Exam_System.Feature.Question.EditQuestion.Dtos
{
    public class EditQuestionToReturnDto
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int ExamId { get; set; }
        public List<ChoiceToReturnDto> Choices { get; set; } = new();
    }
}
//{
//    "title": "What is React?",
//  "type": "radio button",
//  "examId": 1,
//  "choices": [
//    { "text": "Frontend framework", "isCorrect": true },
//    { "text": "Database", "isCorrect": false },
//    { "text": "Backend language", "isCorrect": false },
//    { "text": "Operating system", "isCorrect": false }
//  ]
//}