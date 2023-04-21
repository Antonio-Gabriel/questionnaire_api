using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestionaryApp.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
             // Table
            builder.ToTable("Category");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);            

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