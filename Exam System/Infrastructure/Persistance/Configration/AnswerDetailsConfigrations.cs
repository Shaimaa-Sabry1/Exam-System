using System.Text.Json;
using Exam_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Infrastructure.Persistance.Configration
{
    public class AnswerDetailsConfigrations : IEntityTypeConfiguration<AnswerDetail>
    {
        public void Configure(EntityTypeBuilder<AnswerDetail> builder)
        {
            builder.HasKey(ad => ad.Id);
            builder.ToTable("AnswerDetails");

            // Each AnswerDetail → Question
            builder.HasOne(ad => ad.Question)
                   .WithMany()
                   .HasForeignKey(ad => ad.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Each AnswerDetail → Answer
            builder.HasOne(ad => ad.Answer)
                   .WithMany(a => a.Details)
                   .HasForeignKey(ad => ad.AnswerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ad=>ad.SelectedChoiceIds)
                 .HasConversion(
                          v=> JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                            v=> JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions?)null)

                 ).HasColumnType("nvarchar(max)");



        }
    }
}
