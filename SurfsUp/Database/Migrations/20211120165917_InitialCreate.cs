using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BafuSurfSpots",
                columns: table => new
                {
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BafuSurfSpots", x => x.Url);
                });

            migrationBuilder.CreateTable(
                name: "MswSurfSpots",
                columns: table => new
                {
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MswSurfSpots", x => x.Url);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BafuSurfSpots");

            migrationBuilder.DropTable(
                name: "MswSurfSpots");
        }
    }
}
