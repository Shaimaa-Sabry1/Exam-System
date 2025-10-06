using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class AttemptQuestionConfiguration : IEntityTypeConfiguration<AttemptQuestion>
    {
        public void Configure(EntityTypeBuilder<AttemptQuestion> builder)
        {
            builder.ToTable("AttemptQuestions");

            builder.HasKey(aq => aq.attemptQuestionId);

            builder.Property(aq => aq.order)
                   .IsRequired();

            builder.Property(aq => aq.ChoiceOrderJson)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(aq => aq.SelectedChoiceId)
                   .IsRequired(false);

            builder.Property(aq => aq.IsCorrect)
                   .IsRequired(false);

            // 🔗 AttemptQuestion → Attempt
            builder.HasOne(aq => aq.startExam)
                   .WithMany(a => a.AttemptQuestions)
                   .HasForeignKey(aq => aq.startExamId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔗 AttemptQuestion → Question
            builder.HasOne(aq => aq.question)
                   .WithMany()
                   .HasForeignKey(aq => aq.questionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔗 AttemptQuestion → AttemptQuestionChoices
            builder.HasMany(aq => aq.AttemptQuestionChoices)
                   .WithOne(aqc => aqc.AttemptQuestion)
                   .HasForeignKey(aqc => aqc.AttemptQuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
