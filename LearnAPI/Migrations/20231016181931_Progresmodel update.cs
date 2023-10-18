using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class Progresmodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Courses_LastCompletedCourseId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Levels_CurrentLevelId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Lessons_LessonModelLessonId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_LessonModelLessonId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "LessonModelLessonId",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "LastCompletedCourseId",
                table: "Progress",
                newName: "LastCompletedLevelId");

            migrationBuilder.RenameColumn(
                name: "CurrentLevelId",
                table: "Progress",
                newName: "LastCompletedLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_LastCompletedCourseId",
                table: "Progress",
                newName: "IX_Progress_LastCompletedLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_CurrentLevelId",
                table: "Progress",
                newName: "IX_Progress_LastCompletedLessonId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Progress",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastCompletedCourseCourseId",
                table: "Progress",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LastCompletedCourseCourseId",
                table: "Progress",
                column: "LastCompletedCourseCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Courses_LastCompletedCourseCourseId",
                table: "Progress",
                column: "LastCompletedCourseCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Lessons_LastCompletedLessonId",
                table: "Progress",
                column: "LastCompletedLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Levels_LastCompletedLevelId",
                table: "Progress",
                column: "LastCompletedLevelId",
                principalTable: "Levels",
                principalColumn: "LevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Courses_LastCompletedCourseCourseId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Lessons_LastCompletedLessonId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Levels_LastCompletedLevelId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_LastCompletedCourseCourseId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "LastCompletedCourseCourseId",
                table: "Progress");

            migrationBuilder.RenameColumn(
                name: "LastCompletedLevelId",
                table: "Progress",
                newName: "LastCompletedCourseId");

            migrationBuilder.RenameColumn(
                name: "LastCompletedLessonId",
                table: "Progress",
                newName: "CurrentLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_LastCompletedLevelId",
                table: "Progress",
                newName: "IX_Progress_LastCompletedCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_LastCompletedLessonId",
                table: "Progress",
                newName: "IX_Progress_CurrentLevelId");

            migrationBuilder.AddColumn<int>(
                name: "LessonModelLessonId",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LessonModelLessonId",
                table: "Tests",
                column: "LessonModelLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Courses_LastCompletedCourseId",
                table: "Progress",
                column: "LastCompletedCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Levels_CurrentLevelId",
                table: "Progress",
                column: "CurrentLevelId",
                principalTable: "Levels",
                principalColumn: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Lessons_LessonModelLessonId",
                table: "Tests",
                column: "LessonModelLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");
        }
    }
}
