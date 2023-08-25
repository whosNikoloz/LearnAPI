using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class CourseEnrollMent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollmentModel_Courses_CourseId",
                table: "CourseEnrollmentModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollmentModel_Users_UserId",
                table: "CourseEnrollmentModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_UserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEnrollmentModel",
                table: "CourseEnrollmentModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "CourseEnrollmentModel",
                newName: "CourseEnroll");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnrollmentModel_CourseId",
                table: "CourseEnroll",
                newName: "IX_CourseEnroll_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEnroll",
                table: "CourseEnroll",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnroll_Courses_CourseId",
                table: "CourseEnroll",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnroll_Users_UserId",
                table: "CourseEnroll",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnroll_Courses_CourseId",
                table: "CourseEnroll");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnroll_Users_UserId",
                table: "CourseEnroll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEnroll",
                table: "CourseEnroll");

            migrationBuilder.RenameTable(
                name: "CourseEnroll",
                newName: "CourseEnrollmentModel");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnroll_CourseId",
                table: "CourseEnrollmentModel",
                newName: "IX_CourseEnrollmentModel_CourseId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEnrollmentModel",
                table: "CourseEnrollmentModel",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollmentModel_Courses_CourseId",
                table: "CourseEnrollmentModel",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollmentModel_Users_UserId",
                table: "CourseEnrollmentModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
