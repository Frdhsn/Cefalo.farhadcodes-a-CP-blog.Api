using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cefalo.farhadcodes_a_CP_blog.Database.Migrations
{
    public partial class habijabi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedTime",
                table: "Stories",
                newName: "LastModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Stories",
                newName: "LastModifiedTime");
        }
    }
}
