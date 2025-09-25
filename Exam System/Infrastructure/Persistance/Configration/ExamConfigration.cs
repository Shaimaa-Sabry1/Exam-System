using Exam_System.Feature.Exams.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class ExamConfigration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(e => e.ExamId);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
            builder.Property(e => e.DurationInMinutes).IsRequired();
            builder.HasMany(e => e.Answers)
                   .WithOne(a => a.Exam)
                   .HasForeignKey(e=>e.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.Questions)
                .WithOne(q => q.Exam)
                .HasForeignKey(e => e.ExamId);

                
        }
    }
}
