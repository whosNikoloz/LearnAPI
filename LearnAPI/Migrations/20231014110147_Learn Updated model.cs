using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class LearnUpdatedmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learn_Subjects_SubjectId",
                table: "Learn");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Learn_LearnId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Subjects_SubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_LearnId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Learn_SubjectId",
                table: "Learn");

            migrationBuilder.DropColumn(
                name: "LearnId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Learn");

            migrationBuilder.AddColumn<int>(
                name: "LessonModelLessonId",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonModelLessonId",
                table: "Learn",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Learn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LessonModel",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonModel", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_LessonModel_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LessonModelLessonId",
                table: "Tests",
                column: "LessonModelLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Learn_LessonModelLessonId",
                table: "Learn",
                column: "LessonModelLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Learn_TestId",
                table: "Learn",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonModel_SubjectId",
                table: "LessonModel",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_LessonModel_LessonModelLessonId",
                table: "Learn",
                column: "LessonModelLessonId",
                principalTable: "LessonModel",
                principalColumn: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_Tests_TestId",
                table: "Learn",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_LessonModel_LessonModelLessonId",
                table: "Tests",
                column: "LessonModelLessonId",
                principalTable: "LessonModel",
                principalColumn: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learn_LessonModel_LessonModelLessonId",
                table: "Learn");

            migrationBuilder.DropForeignKey(
                name: "FK_Learn_Tests_TestId",
                table: "Learn");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_LessonModel_LessonModelLessonId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "LessonModel");

            migrationBuilder.DropIndex(
                name: "IX_Tests_LessonModelLessonId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Learn_LessonModelLessonId",
                table: "Learn");

            migrationBuilder.DropIndex(
                name: "IX_Learn_TestId",
                table: "Learn");

            migrationBuilder.DropColumn(
                name: "LessonModelLessonId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "LessonModelLessonId",
                table: "Learn");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Learn");

            migrationBuilder.AddColumn<int>(
                name: "LearnId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Learn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LearnId",
                table: "Tests",
                column: "LearnId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SubjectId",
                table: "Tests",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Learn_SubjectId",
                table: "Learn",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Learn_Subjects_SubjectId",
                table: "Learn",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Learn_LearnId",
                table: "Tests",
                column: "LearnId",
                principalTable: "Learn",
                principalColumn: "LearnId",
                onDelete: ReferentialAction.Cascade);

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
