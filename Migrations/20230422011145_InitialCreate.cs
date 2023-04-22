using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestionaryApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()"),
                    LastModified = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()"),
                    LastModified = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaire",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()"),
                    LastModified = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionnaire_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Correct = table.Column<int>(type: "int", nullable: false),
                    Wrong = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()"),
                    LastModified = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    QuestionnaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()"),
                    LastModified = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Questionnaire_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "Questionnaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    isCorrect = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()"),
                    LastModified = table.Column<DateTime>(type: "DATETIME", maxLength: 60, nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionnaireId",
                table: "Question",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_CategoryId",
                table: "Questionnaire",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_Title",
                table: "Questionnaire",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Score_UserId",
                table: "Score",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Questionnaire");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
