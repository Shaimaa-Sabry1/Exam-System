using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class AnswerConfigrations : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Score)
                   .IsRequired();

            // 1 Answer -> 1 Attempt
            builder.HasOne(a => a.Attembt)
                   .WithOne(attempt => attempt.Answer)
                   .HasForeignKey<Answer>(a => a.attembtId)
                   .OnDelete(DeleteBehavior.NoAction); 

        }
    }
}
