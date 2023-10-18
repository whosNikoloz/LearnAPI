using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatereltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Subjects_LastCompletedSubjectId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_LastCompletedCourseCourseId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_LastCompletedLessonId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_LastCompletedLevelId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_UserId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "LastCompletedCourseCourseId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "LastCompletedLessonId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "LastCompletedLevelId",
                table: "Progress");

            migrationBuilder.RenameColumn(
                name: "LastCompletedSubjectId",
                table: "Progress",
                newName: "LessonModelLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_LastCompletedSubjectId",
                table: "Progress",
                newName: "IX_Progress_LessonModelLessonId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Progress",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLessonId",
                table: "Progress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentSubjectId",
                table: "Progress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_CourseId",
                table: "Progress",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_CurrentLessonId",
                table: "Progress",
                column: "CurrentLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_CurrentSubjectId",
                table: "Progress",
                column: "CurrentSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_UserId",
                table: "Progress",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Courses_CourseId",
                table: "Progress",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Courses_CourseId",
                table: "Progress");

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
                name: "IX_Progress_CourseId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_CurrentLessonId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_CurrentSubjectId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_UserId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "CurrentLessonId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "CurrentSubjectId",
                table: "Progress");

            migrationBuilder.RenameColumn(
                name: "LessonModelLessonId",
                table: "Progress",
                newName: "LastCompletedSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_LessonModelLessonId",
                table: "Progress",
                newName: "IX_Progress_LastCompletedSubjectId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Progress",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LastCompletedCourseCourseId",
                table: "Progress",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastCompletedLessonId",
                table: "Progress",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastCompletedLevelId",
                table: "Progress",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LastCompletedCourseCourseId",
                table: "Progress",
                column: "LastCompletedCourseCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LastCompletedLessonId",
                table: "Progress",
                column: "LastCompletedLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LastCompletedLevelId",
                table: "Progress",
                column: "LastCompletedLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_UserId",
                table: "Progress",
                column: "UserId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Subjects_LastCompletedSubjectId",
                table: "Progress",
                column: "LastCompletedSubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId");
        }
    }
}
