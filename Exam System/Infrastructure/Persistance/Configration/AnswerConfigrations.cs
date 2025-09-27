using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class AnswerConfigrations : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.AnswerId);

            builder.Property(a => a.Score)
                   .IsRequired();

            // 1 Answer -> Many AnswerDetails
            builder.HasMany(a => a.Details)
                   .WithOne(d => d.Answer)
                   .HasForeignKey(d => d.AnswerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
    
}
