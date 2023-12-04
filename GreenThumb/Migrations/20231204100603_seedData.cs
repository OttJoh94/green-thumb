using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenThumb.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gardens",
                columns: new[] { "id", "environment", "location", "square_meters" },
                values: new object[,]
                {
                    { 1, "Greenhouse", "Sweden", 120 },
                    { 2, "Field", "England", 6400 }
                });

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
                columns: new[] { "garden_id", "plant_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 4 },
                    { 1, 7 }
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
                values: new object[] { 1, 1, "password", "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "Gardens",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gardens",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 7);
        }
    }
}
