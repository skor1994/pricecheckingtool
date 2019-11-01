using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pricecheckingtoolapi.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    currencyTypeName = table.Column<string>(maxLength: 35, nullable: false),
                    chaosEquivalent = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.currencyTypeName);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    links = table.Column<int>(nullable: false),
                    baseType = table.Column<string>(nullable: true),
                    chaosValue = table.Column<double>(nullable: false),
                    exaltedValue = table.Column<double>(nullable: false),
                    mapTier = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Partys",
                columns: table => new
                {
                    partyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partys", x => x.partyId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    username = table.Column<string>(maxLength: 35, nullable: false),
                    sessionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "PartyUser",
                columns: table => new
                {
                    partyId = table.Column<int>(nullable: false),
                    username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyUser", x => new { x.username, x.partyId });
                    table.ForeignKey(
                        name: "FK_PartyUser_Partys_partyId",
                        column: x => x.partyId,
                        principalTable: "Partys",
                        principalColumn: "partyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyUser_Users_username",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyUser_partyId",
                table: "PartyUser",
                column: "partyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PartyUser");

            migrationBuilder.DropTable(
                name: "Partys");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
