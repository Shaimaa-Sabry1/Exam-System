using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class ChoiceConfigration : IEntityTypeConfiguration<Choice>
    {



        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.HasKey(c => c.ChoiceId);

            builder.Property(c => c.Text)
                   .IsRequired()
                   .HasMaxLength(300);
        }
    }
}
