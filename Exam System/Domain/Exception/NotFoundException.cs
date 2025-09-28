using System;
namespace Exam_System.Domain.Exception
{
    public class NotFoundException(string message) : System.Exception(message)
    {
    }
}
