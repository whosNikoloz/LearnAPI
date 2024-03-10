using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnAPI.Migrations
{
    /// <inheritdoc />
    public partial class languageupdatekaandenonsomemodelinlearn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectName",
                table: "Subjects",
                newName: "SubjectName_ka");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Subjects",
                newName: "SubjectName_en");

            migrationBuilder.RenameColumn(
                name: "LevelName",
                table: "Levels",
                newName: "LevelName_ka");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Levels",
                newName: "LevelName_en");

            migrationBuilder.RenameColumn(
                name: "LessonName",
                table: "Lessons",
                newName: "LessonName_ka");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "Description_ka");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Courses",
                newName: "CourseName_ka");

            migrationBuilder.AddColumn<string>(
                name: "Description_en",
                table: "Subjects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description_ka",
                table: "Subjects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description_en",
                table: "Levels",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description_ka",
                table: "Levels",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LessonName_en",
                table: "Lessons",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CourseName_en",
                table: "Courses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description_en",
                table: "Courses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_en",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Description_ka",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Description_en",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "Description_ka",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "LessonName_en",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CourseName_en",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description_en",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SubjectName_ka",
                table: "Subjects",
                newName: "SubjectName");

            migrationBuilder.RenameColumn(
                name: "SubjectName_en",
                table: "Subjects",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "LevelName_ka",
                table: "Levels",
                newName: "LevelName");

            migrationBuilder.RenameColumn(
                name: "LevelName_en",
                table: "Levels",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "LessonName_ka",
                table: "Lessons",
                newName: "LessonName");

            migrationBuilder.RenameColumn(
                name: "Description_ka",
                table: "Courses",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CourseName_ka",
                table: "Courses",
                newName: "CourseName");
        }
    }
}
