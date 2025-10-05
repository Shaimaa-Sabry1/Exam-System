using Exam_System.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Feature.Exams.Commands
{
    public class CreateExamCommand : IRequest<int>
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public IFormFile? Icon { get; set; }
        public int DurationInMinutes { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        

    }
}
