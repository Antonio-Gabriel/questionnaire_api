using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestionaryApp.Data.Mappings
{
    public class ScoreMap : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            // Table
            builder.ToTable("Score");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties                 
            builder.Property(x => x.Correct);
            builder.Property(x => x.Wrong);

            builder
               .HasOne(x => x.User)
               .WithOne(x => x.Score)
               .HasForeignKey<Score>(x => x.UserId)
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