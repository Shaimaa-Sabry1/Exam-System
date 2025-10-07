using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class ExamConfigration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
            builder.Property(e => e.DurationInMinutes).IsRequired();
            
            builder.HasMany(e => e.Questions)
                .WithOne(q => q.Exam)
                .HasForeignKey(e => e.ExamId);

                
        }
    }
}
