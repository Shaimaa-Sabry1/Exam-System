﻿namespace Exam_System.Feature.Users.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = "User";
        public string? ProfilePictureUrl { get; set; }
    }
}
