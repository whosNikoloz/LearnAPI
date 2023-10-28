using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class TesmodelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Learn_TestId",
                table: "Learn");

            migrationBuilder.AddColumn<int>(
                name: "LearnId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Learn_TestId",
                table: "Learn",
                column: "TestId",
                unique: true,
                filter: "[TestId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Learn_TestId",
                table: "Learn");

            migrationBuilder.DropColumn(
                name: "LearnId",
                table: "Tests");

            migrationBuilder.CreateIndex(
                name: "IX_Learn_TestId",
                table: "Learn",
                column: "TestId");
        }
    }
}
