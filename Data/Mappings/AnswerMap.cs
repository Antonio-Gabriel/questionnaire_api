using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestionaryApp.Data.Mappings
{
    public class AnswerMap : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            // Table
            builder.ToTable("Answer");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Text)
                .IsRequired()
                .HasColumnName("Text")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.isCorrect)
               .HasDefaultValue(false);

            builder
                .HasOne(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt)                
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME")
                .HasMaxLength(60)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.LastModified)                
                .HasColumnName("LastModified")
                .HasColumnType("DATETIME")
                .HasMaxLength(60)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}