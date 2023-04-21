﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuestionaryApp.Data;

#nullable disable

namespace QuestionaryApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("LastModified")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Text");

                    b.Property<bool>("isCorrect")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer", (string)null);
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("LastModified")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("LastModified")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("QuestionnaireId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Questionnaire", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("LastModified")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.HasIndex(new[] { "Title" }, "IX_Questionnaire_Title");

                    b.ToTable("Questionnaire", (string)null);
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Score", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Correct")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("LastModified")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Wrong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Score", (string)null);
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Email");

                    b.Property<DateTime?>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("DATETIME")
                        .HasColumnName("LastModified")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Answer", b =>
                {
                    b.HasOne("QuestionaryApp.Domain.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Question", b =>
                {
                    b.HasOne("QuestionaryApp.Domain.Entities.Questionnaire", "Questionnaire")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Questionnaire", b =>
                {
                    b.HasOne("QuestionaryApp.Domain.Entities.Category", "Category")
                        .WithOne("Questionnaire")
                        .HasForeignKey("QuestionaryApp.Domain.Entities.Questionnaire", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Score", b =>
                {
                    b.HasOne("QuestionaryApp.Domain.Entities.User", "User")
                        .WithOne("Score")
                        .HasForeignKey("QuestionaryApp.Domain.Entities.Score", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Category", b =>
                {
                    b.Navigation("Questionnaire")
                        .IsRequired();
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.Questionnaire", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QuestionaryApp.Domain.Entities.User", b =>
                {
                    b.Navigation("Score")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
