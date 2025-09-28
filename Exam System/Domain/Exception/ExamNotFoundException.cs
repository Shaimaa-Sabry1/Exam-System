namespace Exam_System.Domain.Exception
{
    public class ExamNotFoundException(int Id) : NotFoundException($"Exam With Id {Id} not found ")
    {
      
    }
}
