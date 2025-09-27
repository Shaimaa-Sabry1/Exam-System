using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class AnswerDetailsConfigrations : IEntityTypeConfiguration<AnswerDetail>
    {
        public void Configure(EntityTypeBuilder<AnswerDetail> builder)
        {
            builder.HasKey(ad => ad.AnswerDetailId);

            // Each AnswerDetail → Question
            builder.HasOne(ad => ad.Question)
                   .WithMany()
                   .HasForeignKey(ad => ad.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Each AnswerDetail → Choice
            builder.HasOne(ad => ad.Choice)
                   .WithMany()
                   .HasForeignKey(ad => ad.ChoiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
