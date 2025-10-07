namespace Exam_System.Domain.Entities
{
    public class attembt
    {
       public int Id { get; set; }
       public int UserId { get; set; }
        public int ExamId { get; set; }
       
       public DateTime startTime { get; set; } = DateTime.Now;
        public Exam Exam { get; set; }
        public Answer Answer { get; set; }

        public bool ? IsSubmitted { get; set; }
      //  public bool? IsDeleted { get; set; }


    }
}
