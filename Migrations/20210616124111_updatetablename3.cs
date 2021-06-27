using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira_server.Migrations
{
    public partial class updatetablename3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "user",
                newName: "NickName");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "user",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "user",
                newName: "nickname");
        }
    }
}
