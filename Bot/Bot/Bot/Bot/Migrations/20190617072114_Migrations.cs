using Microsoft.EntityFrameworkCore.Migrations;

namespace Bot.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IName = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "memes",
                columns: table => new
                {
                    MemeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memes", x => x.MemeID);
                });

            migrationBuilder.CreateTable(
                name: "myblacklist",
                columns: table => new
                {
                    block_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_myblacklist", x => x.block_id);
                });

            migrationBuilder.CreateTable(
                name: "myUser",
                columns: table => new
                {
                    UserID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Coins = table.Column<int>(nullable: false),
                    Loggedin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_myUser", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    PetID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    affection = table.Column<int>(nullable: false),
                    price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.PetID);
                });

            migrationBuilder.CreateTable(
                name: "userItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemID = table.Column<int>(nullable: false),
                    UserID = table.Column<ulong>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "userPets",
                columns: table => new
                {
                    UserID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PetID = table.Column<int>(nullable: false),
                    PetName = table.Column<string>(nullable: true),
                    affection = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userPets", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "memes");

            migrationBuilder.DropTable(
                name: "myblacklist");

            migrationBuilder.DropTable(
                name: "myUser");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "userItems");

            migrationBuilder.DropTable(
                name: "userPets");
        }
    }
}
