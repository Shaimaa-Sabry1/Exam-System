using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class AttemptConfiguration : IEntityTypeConfiguration<attembt>
    {
        public void Configure(EntityTypeBuilder<attembt> builder)
        {
            builder.ToTable("Attempts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.startTime)
                .IsRequired();

            builder.Property(a => a.IsSubmitted)
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(a => a.Exam)
                .WithMany()   
                .HasForeignKey(a => a.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Example index for performance (optional)
            builder.HasIndex(a => new { a.UserId, a.ExamId })
                .IsUnique(false);
        }
    }
}
