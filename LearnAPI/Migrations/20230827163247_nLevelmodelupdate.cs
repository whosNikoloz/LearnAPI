using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class nLevelmodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learn_Tests_TestId",
                table: "Learn");

            migrationBuilder.DropIndex(
                name: "IX_Learn_TestId",
                table: "Learn");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Learn");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LearnId",
                table: "Tests",
                column: "LearnId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Learn_LearnId",
                table: "Tests",
                column: "LearnId",
                principalTable: "Learn",
                principalColumn: "LearnId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Learn_LearnId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_LearnId",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Learn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Learn_TestId",
                table: "Learn",
                column: "TestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_Tests_TestId",
                table: "Learn",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
