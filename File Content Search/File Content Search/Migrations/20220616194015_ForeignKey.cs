using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace File_Content_Search.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LibraryId_FK",
                table: "LibraryItems",
                newName: "LibraryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LibraryId",
                table: "LibraryItems",
                newName: "LibraryId_FK");
        }
    }
}
