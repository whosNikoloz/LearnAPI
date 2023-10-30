using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixedVideoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Learn_LevelId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_LevelId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Videos",
                newName: "LearnId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LearnId",
                table: "Videos",
                column: "LearnId",
                unique: true,
                filter: "[LearnId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Learn_LearnId",
                table: "Videos",
                column: "LearnId",
                principalTable: "Learn",
                principalColumn: "LearnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Learn_LearnId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_LearnId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "LearnId",
                table: "Videos",
                newName: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LevelId",
                table: "Videos",
                column: "LevelId",
                unique: true,
                filter: "[LevelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Learn_LevelId",
                table: "Videos",
                column: "LevelId",
                principalTable: "Learn",
                principalColumn: "LearnId");
        }
    }
}
