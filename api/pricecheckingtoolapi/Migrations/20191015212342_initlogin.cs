using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pricecheckingtoolapi.Migrations
{
    public partial class initlogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "Users");
        }
    }
}
