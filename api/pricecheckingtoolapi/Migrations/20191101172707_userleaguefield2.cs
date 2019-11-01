using Microsoft.EntityFrameworkCore.Migrations;

namespace pricecheckingtoolapi.Migrations
{
    public partial class userleaguefield2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "league",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "league",
                table: "Users");
        }
    }
}
