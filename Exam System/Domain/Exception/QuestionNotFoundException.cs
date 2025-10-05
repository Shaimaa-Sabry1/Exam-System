namespace Exam_System.Domain.Exception
{
    public class QuestionNotFoundException(int Id) : NotFoundException($"Question With Id {Id} Not Found")
    {
    }
}
