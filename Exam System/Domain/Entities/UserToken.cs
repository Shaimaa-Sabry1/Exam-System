namespace Exam_System.Domain.Entities
{
    public class UserToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
}
