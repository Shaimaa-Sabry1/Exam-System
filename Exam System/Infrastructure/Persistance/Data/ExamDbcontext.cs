using Exam_System.Feature.Answers.Model;
using Exam_System.Feature.Categories.Model;
using Exam_System.Feature.Exams.Model;
using Exam_System.Feature.Questions.Model;
using Exam_System.Feature.Users.Model;
using Exam_System.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Infrastructure.Persistance.Data
{
    public class ExamDbcontext(DbContextOptions<ExamDbcontext >options) : DbContext(options)
    {

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }  
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User>users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReferance).Assembly);
        }


    }
}
