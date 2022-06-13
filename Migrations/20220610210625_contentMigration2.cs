using Microsoft.EntityFrameworkCore.Migrations;

namespace FlagApi.Migrations
{
    public partial class contentMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "contents",
                newName: "content_type");

            migrationBuilder.RenameColumn(
                name: "ContentPath",
                table: "contents",
                newName: "content_path");

            migrationBuilder.RenameColumn(
                name: "ContentName",
                table: "contents",
                newName: "content_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "contents",
                newName: "content_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "content_type",
                table: "contents",
                newName: "ContentType");

            migrationBuilder.RenameColumn(
                name: "content_path",
                table: "contents",
                newName: "ContentPath");

            migrationBuilder.RenameColumn(
                name: "content_name",
                table: "contents",
                newName: "ContentName");

            migrationBuilder.RenameColumn(
                name: "content_id",
                table: "contents",
                newName: "Id");
        }
    }
}
