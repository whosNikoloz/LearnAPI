using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateeeeeeeeeeeeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Lessons_CurrentLessonId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Lessons_LessonModelLessonId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Subjects_CurrentSubjectId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_LessonModelLessonId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "LessonModelLessonId",
                table: "Progress");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Lessons_CurrentLessonId",
                table: "Progress",
                column: "CurrentLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Subjects_CurrentSubjectId",
                table: "Progress",
                column: "CurrentSubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Lessons_CurrentLessonId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Subjects_CurrentSubjectId",
                table: "Progress");

            migrationBuilder.AddColumn<int>(
                name: "LessonModelLessonId",
                table: "Progress",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LessonModelLessonId",
                table: "Progress",
                column: "LessonModelLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Lessons_CurrentLessonId",
                table: "Progress",
                column: "CurrentLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Lessons_LessonModelLessonId",
                table: "Progress",
                column: "LessonModelLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Subjects_CurrentSubjectId",
                table: "Progress",
                column: "CurrentSubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
