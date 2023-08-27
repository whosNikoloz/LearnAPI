using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class TestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Subjects_SubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Learn_TestId",
                table: "Learn");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Tests",
                newName: "LearnId");

            

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Learn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            

            migrationBuilder.CreateIndex(
                name: "IX_Learn_SubjectId",
                table: "Learn",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Learn_TestId",
                table: "Learn",
                column: "TestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_Subjects_SubjectId",
                table: "Learn",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learn_Subjects_SubjectId",
                table: "Learn");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Subjects_SubjectModelSubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SubjectModelSubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Learn_SubjectId",
                table: "Learn");

            migrationBuilder.DropIndex(
                name: "IX_Learn_TestId",
                table: "Learn");

            migrationBuilder.DropColumn(
                name: "SubjectModelSubjectId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Learn");

            migrationBuilder.RenameColumn(
                name: "LearnId",
                table: "Tests",
                newName: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SubjectId",
                table: "Tests",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Learn_TestId",
                table: "Learn",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Subjects_SubjectId",
                table: "Tests",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
