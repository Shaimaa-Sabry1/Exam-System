using System.Text.Json.Serialization;
using MediatR;

namespace Exam_System.Feature.Exam.UpdateExam
{
    public class UpdateExamCommand : IRequest<bool>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Title { get; set; }
        public string? Icon { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        

    }
}
