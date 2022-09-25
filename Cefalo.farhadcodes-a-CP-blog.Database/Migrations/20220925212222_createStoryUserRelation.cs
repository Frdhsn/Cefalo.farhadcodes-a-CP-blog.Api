using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cefalo.farhadcodes_a_CP_blog.Database.Migrations
{
    public partial class createStoryUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Users_UserId",
                table: "Story");

            migrationBuilder.DropIndex(
                name: "IX_Story_UserId",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Story");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Story",
                newName: "LastModifiedTime");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Story",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Story",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Users_Id",
                table: "Story",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Users_Id",
                table: "Story");

            migrationBuilder.RenameColumn(
                name: "LastModifiedTime",
                table: "Story",
                newName: "LastModified");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Story",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Story",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Story",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Story_UserId",
                table: "Story",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Users_UserId",
                table: "Story",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
