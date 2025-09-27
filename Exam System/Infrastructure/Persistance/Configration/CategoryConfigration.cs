using Exam_System.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class CategoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Icon)
                   .HasMaxLength(200);

            // 1 Category -> Many Exams
            builder.HasMany(c => c.Exam)
                   .WithOne(e => e.Category)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
