using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateLearntable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learn_Lessons_LessonModelLessonId",
                table: "Learn");

            migrationBuilder.DropIndex(
                name: "IX_Learn_LessonModelLessonId",
                table: "Learn");

            migrationBuilder.DropColumn(
                name: "LessonModelLessonId",
                table: "Learn");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Learn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Learn_LessonId",
                table: "Learn",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_Lessons_LessonId",
                table: "Learn",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learn_Lessons_LessonId",
                table: "Learn");

            migrationBuilder.DropIndex(
                name: "IX_Learn_LessonId",
                table: "Learn");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Learn");

            migrationBuilder.AddColumn<int>(
                name: "LessonModelLessonId",
                table: "Learn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Learn_LessonModelLessonId",
                table: "Learn",
                column: "LessonModelLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_Lessons_LessonModelLessonId",
                table: "Learn",
                column: "LessonModelLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");
        }
    }
}
