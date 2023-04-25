using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestionaryApp.Data.Mappings
{
    public class QuestionnaireMap : IEntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            // Table
            builder.ToTable("Questionnaire");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Questionnaire)
                .HasForeignKey(x => x.CategoryId)
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

            // Indices
            builder.HasIndex(x => x.Title, "IX_Questionnaire_Title");
        }
    }
}