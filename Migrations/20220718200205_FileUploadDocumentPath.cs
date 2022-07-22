using Microsoft.EntityFrameworkCore.Migrations;

namespace EgolePayUsersManagementSystem.Migrations
{
    public partial class FileUploadDocumentPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "FileUploads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "FileUploads");
        }
    }
}
