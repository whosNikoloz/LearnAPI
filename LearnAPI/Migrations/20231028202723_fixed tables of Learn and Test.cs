using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixedtablesofLearnandTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Learn",
                newName: "Content");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Learn",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Learn");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Learn",
                newName: "Description");
        }
    }
}
