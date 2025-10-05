using System.Reflection;

namespace Exam_System.Domain.Entities
{
    public class EditQuestionToReturnDto
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int ExamId { get; set; }
        public List<ChoiceDto> Choices { get; set; } = new();
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