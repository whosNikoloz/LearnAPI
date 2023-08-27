using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class TestmodelV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Subjects_SubjectModelSubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SubjectModelSubjectId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SubjectModelSubjectId",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SubjectId",
                table: "Tests",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Subjects_SubjectId",
                table: "Tests",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Subjects_SubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SubjectId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "SubjectModelSubjectId",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SubjectModelSubjectId",
                table: "Tests",
                column: "SubjectModelSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Subjects_SubjectModelSubjectId",
                table: "Tests",
                column: "SubjectModelSubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId");
        }
    }
}
