using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestionaryApp.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table
            builder.ToTable("User");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Email)
               .IsRequired()
               .HasColumnName("Email")
               .HasColumnType("VARCHAR")
               .HasMaxLength(80);

            builder.Property(x => x.CodeName);
            builder.Property(x => x.Password);

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