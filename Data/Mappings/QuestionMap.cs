using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestionaryApp.Data.Mappings
{
    public class QuestionMap : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            // Table
            builder.ToTable("Question");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder
                .HasOne(x => x.Questionnaire)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.QuestionnaireId)
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