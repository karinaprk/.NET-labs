using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddedAuthorToNewsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Users_UserId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "News",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_News_UserId",
                table: "News",
                newName: "IX_News_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_AuthorId",
                table: "News",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Users_AuthorId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "News",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_News_AuthorId",
                table: "News",
                newName: "IX_News_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_UserId",
                table: "News",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
