using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class QuestionConfigration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.QuestionId);

            builder.Property(q => q.Title)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(q => q.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            // 1 Question -> Many Choices
            builder.HasMany(q => q.Choices)
                   .WithOne(c => c.Question)
                   .HasForeignKey(c => c.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
    
}
