using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProgressStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressModel_Courses_LastCompletedCourseId",
                table: "ProgressModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressModel_Levels_CurrentLevelId",
                table: "ProgressModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressModel_Subjects_LastCompletedSubjectId",
                table: "ProgressModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressModel_Tests_LastCompletedTestId",
                table: "ProgressModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressModel_Users_UserId",
                table: "ProgressModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgressModel",
                table: "ProgressModel");

            migrationBuilder.RenameTable(
                name: "ProgressModel",
                newName: "Progress");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressModel_UserId",
                table: "Progress",
                newName: "IX_Progress_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressModel_LastCompletedTestId",
                table: "Progress",
                newName: "IX_Progress_LastCompletedTestId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressModel_LastCompletedSubjectId",
                table: "Progress",
                newName: "IX_Progress_LastCompletedSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressModel_LastCompletedCourseId",
                table: "Progress",
                newName: "IX_Progress_LastCompletedCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressModel_CurrentLevelId",
                table: "Progress",
                newName: "IX_Progress_CurrentLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progress",
                table: "Progress",
                column: "ProgressId");

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
                name: "FK_Progress_Subjects_LastCompletedSubjectId",
                table: "Progress",
                column: "LastCompletedSubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Tests_LastCompletedTestId",
                table: "Progress",
                column: "LastCompletedTestId",
                principalTable: "Tests",
                principalColumn: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Users_UserId",
                table: "Progress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Courses_LastCompletedCourseId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Levels_CurrentLevelId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Subjects_LastCompletedSubjectId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Tests_LastCompletedTestId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Users_UserId",
                table: "Progress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progress",
                table: "Progress");

            migrationBuilder.RenameTable(
                name: "Progress",
                newName: "ProgressModel");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_UserId",
                table: "ProgressModel",
                newName: "IX_ProgressModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_LastCompletedTestId",
                table: "ProgressModel",
                newName: "IX_ProgressModel_LastCompletedTestId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_LastCompletedSubjectId",
                table: "ProgressModel",
                newName: "IX_ProgressModel_LastCompletedSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_LastCompletedCourseId",
                table: "ProgressModel",
                newName: "IX_ProgressModel_LastCompletedCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_CurrentLevelId",
                table: "ProgressModel",
                newName: "IX_ProgressModel_CurrentLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgressModel",
                table: "ProgressModel",
                column: "ProgressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressModel_Courses_LastCompletedCourseId",
                table: "ProgressModel",
                column: "LastCompletedCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressModel_Levels_CurrentLevelId",
                table: "ProgressModel",
                column: "CurrentLevelId",
                principalTable: "Levels",
                principalColumn: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressModel_Subjects_LastCompletedSubjectId",
                table: "ProgressModel",
                column: "LastCompletedSubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressModel_Tests_LastCompletedTestId",
                table: "ProgressModel",
                column: "LastCompletedTestId",
                principalTable: "Tests",
                principalColumn: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressModel_Users_UserId",
                table: "ProgressModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
