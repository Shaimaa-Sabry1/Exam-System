using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class StartExamConfigration : IEntityTypeConfiguration<StartExam>
    {
        public void Configure(EntityTypeBuilder<StartExam> builder)
        {
            builder.ToTable("Attempts");

            builder.HasKey(a => a.attemptId);

            builder.Property(a => a.userId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.startTime)
                   .IsRequired();

            builder.Property(a => a.endTime)
                   .IsRequired(false);

            builder.Property(a => a.score)
                   .HasDefaultValue(0);

            builder.Property(a => a.DurationTakenMinutes)
                   .IsRequired(false);

            // 🔗 Attempt → Exam (Many attempts can belong to one exam)
            builder.HasOne(a => a.Exam)
                   .WithMany(e => e.Attempts)
                   .HasForeignKey(a => a.attemptId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔗 Attempt → AttemptQuestions (1 → many)
            builder.HasMany(a => a.AttemptQuestions)
                   .WithOne(aq => aq.startExam)
                   .HasForeignKey(aq => aq.attemptQuestionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
