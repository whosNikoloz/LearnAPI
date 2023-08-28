using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTestFkFromProgressModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Tests_LastCompletedTestId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_LastCompletedTestId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "LastCompletedTestId",
                table: "Progress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastCompletedTestId",
                table: "Progress",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LastCompletedTestId",
                table: "Progress",
                column: "LastCompletedTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Tests_LastCompletedTestId",
                table: "Progress",
                column: "LastCompletedTestId",
                principalTable: "Tests",
                principalColumn: "TestId");
        }
    }
}
