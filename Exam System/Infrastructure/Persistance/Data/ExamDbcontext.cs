using Exam_System.Domain.Entities;
using Exam_System.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Infrastructure.Persistance.Data
{
    public class ExamDbContext(DbContextOptions<ExamDbContext> options) : DbContext(options)
    {

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<attembt> Attembts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReferance).Assembly);
        }


    }
}
