using Microsoft.EntityFrameworkCore.Migrations;

namespace EgolePayUsersManagementSystem.Migrations
{
    public partial class FileUploadDocumentPath2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "FileUploads");

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath1",
                table: "FileUploads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath2",
                table: "FileUploads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentPath1",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "DocumentPath2",
                table: "FileUploads");

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "FileUploads",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
