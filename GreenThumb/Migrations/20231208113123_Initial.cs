using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenThumb.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gardens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    square_meters = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    environment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gardens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    common_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    scientific_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    garden_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Gardens_garden_id",
                        column: x => x.garden_id,
                        principalTable: "Gardens",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "GardenPlants",
                columns: table => new
                {
                    garden_id = table.Column<int>(type: "int", nullable: false),
                    plant_id = table.Column<int>(type: "int", nullable: false),
                    date_planted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenPlants", x => new { x.plant_id, x.garden_id });
                    table.ForeignKey(
                        name: "FK_GardenPlants_Gardens_garden_id",
                        column: x => x.garden_id,
                        principalTable: "Gardens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GardenPlants_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    plant_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Instructions_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gardens",
                columns: new[] { "id", "environment", "location", "square_meters" },
                values: new object[] { 1, "Greenhouse", "Sweden", 3000 });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "id", "common_name", "scientific_name" },
                values: new object[,]
                {
                    { 1, "Sunflower", "Helianthus annuus" },
                    { 2, "Rose", "Rosa rubiginosa" },
                    { 3, "Cactus", "Cactaceae" },
                    { 4, "Common ivy", "Hedera helix" },
                    { 5, "Peony", "Paeoniaceae" },
                    { 6, "Orchid", "Phalaenopsis" },
                    { 7, "Tulip", "Tulipa gesneriana" }
                });

            migrationBuilder.InsertData(
                table: "GardenPlants",
                columns: new[] { "garden_id", "plant_id", "date_planted" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 4, new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 7, new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Instructions",
                columns: new[] { "id", "description", "plant_id" },
                values: new object[,]
                {
                    { 1, "Provide flowers with plenty of daily sunlight", 1 },
                    { 2, "If growing sunflowers in a container, provide enough drainage and loose soil.", 1 },
                    { 3, "Require a moderate amounts of water", 2 },
                    { 4, "Watering infrequently.", 3 },
                    { 5, "Prefer bright indirect light but no direct sun as the foliage will burn", 4 },
                    { 6, "Let the top 25-50% of soil dry before watering.", 4 },
                    { 7, "Peonies need a well-drained position, and are fine with most soil types as long as it is not waterlogged", 5 },
                    { 8, "They enjoy full sun, but can cope with a small amount of shade.", 5 },
                    { 9, "Require water once a week", 6 },
                    { 10, "Position your orchid in a bright windowsill facing east or west.", 6 },
                    { 11, "Weekly feeding with a fertilizer designed for orchids.", 6 },
                    { 12, "Most orchids require water once a week", 6 },
                    { 13, "Cut off 1/2 inch from the bottom of the stem every day in the water", 7 },
                    { 14, "Top off the water with cold water daily", 7 },
                    { 15, "do not put the vase in direct sun", 7 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "garden_id", "password", "username" },
                values: new object[] { 1, 1, "BETzSRAPc3/w6srQ6jx5bw==", "user" });

            migrationBuilder.CreateIndex(
                name: "IX_GardenPlants_garden_id",
                table: "GardenPlants",
                column: "garden_id");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_plant_id",
                table: "Instructions",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_garden_id",
                table: "Users",
                column: "garden_id",
                unique: true,
                filter: "[garden_id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GardenPlants");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Gardens");
        }
    }
}
