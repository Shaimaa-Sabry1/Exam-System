using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class AttemptQuestionChoiceConfiguration : IEntityTypeConfiguration<AttemptQuestionChoice>
    {
        public void Configure(EntityTypeBuilder<AttemptQuestionChoice> builder)
        {
            builder.ToTable("AttemptQuestionChoices");

            builder.HasKey(aqc => aqc.Id);

            builder.Property(aqc => aqc.Order)
                   .IsRequired();

            builder.Property(aqc => aqc.IsSelected)
                   .IsRequired(false);

            builder.Property(aqc => aqc.IsCorrect)
                   .IsRequired(false);

            // 🔗 AttemptQuestionChoice → AttemptQuestion
            builder.HasOne(aqc => aqc.AttemptQuestion)
                   .WithMany(aq => aq.AttemptQuestionChoices)
                   .HasForeignKey(aqc => aqc.AttemptQuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔗 AttemptQuestionChoice → Choice
            builder.HasOne(aqc => aqc.Choice)
                   .WithMany()
                   .HasForeignKey(aqc => aqc.ChoiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
