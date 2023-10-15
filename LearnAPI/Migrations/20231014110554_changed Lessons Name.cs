using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class changedLessonsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learn_LessonModel_LessonModelLessonId",
                table: "Learn");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonModel_Subjects_SubjectId",
                table: "LessonModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_LessonModel_LessonModelLessonId",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonModel",
                table: "LessonModel");

            migrationBuilder.RenameTable(
                name: "LessonModel",
                newName: "Lessons");

            migrationBuilder.RenameIndex(
                name: "IX_LessonModel_SubjectId",
                table: "Lessons",
                newName: "IX_Lessons_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_Lessons_LessonModelLessonId",
                table: "Learn",
                column: "LessonModelLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Lessons_LessonModelLessonId",
                table: "Tests",
                column: "LessonModelLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learn_Lessons_LessonModelLessonId",
                table: "Learn");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Lessons_LessonModelLessonId",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "LessonModel");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_SubjectId",
                table: "LessonModel",
                newName: "IX_LessonModel_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonModel",
                table: "LessonModel",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_LessonModel_LessonModelLessonId",
                table: "Learn",
                column: "LessonModelLessonId",
                principalTable: "LessonModel",
                principalColumn: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonModel_Subjects_SubjectId",
                table: "LessonModel",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_LessonModel_LessonModelLessonId",
                table: "Tests",
                column: "LessonModelLessonId",
                principalTable: "LessonModel",
                principalColumn: "LessonId");
        }
    }
}
