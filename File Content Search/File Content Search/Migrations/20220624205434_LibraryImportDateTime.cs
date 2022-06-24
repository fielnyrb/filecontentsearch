using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace File_Content_Search.Migrations
{
    public partial class LibraryImportDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ImportDateTime",
                table: "Libraries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportDateTime",
                table: "Libraries");
        }
    }
}
