using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenThumb.Migrations
{
    public partial class GardenIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Gardens_garden_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_garden_id",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "garden_id",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 1 },
                column: "date_planted",
                value: new DateTime(2023, 12, 4, 14, 37, 5, 36, DateTimeKind.Local).AddTicks(1628));

            migrationBuilder.UpdateData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 2 },
                column: "date_planted",
                value: new DateTime(2023, 12, 4, 14, 37, 5, 36, DateTimeKind.Local).AddTicks(1675));

            migrationBuilder.UpdateData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 4 },
                column: "date_planted",
                value: new DateTime(2023, 12, 4, 14, 37, 5, 36, DateTimeKind.Local).AddTicks(1677));

            migrationBuilder.UpdateData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 7 },
                column: "date_planted",
                value: new DateTime(2023, 12, 4, 14, 37, 5, 36, DateTimeKind.Local).AddTicks(1679));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "bOPFnRPqQB6Cb1z34z2O8g==");

            migrationBuilder.CreateIndex(
                name: "IX_Users_garden_id",
                table: "Users",
                column: "garden_id",
                unique: true,
                filter: "[garden_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Gardens_garden_id",
                table: "Users",
                column: "garden_id",
                principalTable: "Gardens",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Gardens_garden_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_garden_id",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "garden_id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 1 },
                column: "date_planted",
                value: new DateTime(2023, 12, 4, 13, 49, 22, 568, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 2 },
                column: "date_planted",
                value: new DateTime(2023, 12, 4, 13, 49, 22, 568, DateTimeKind.Local).AddTicks(1577));

            migrationBuilder.UpdateData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 4 },
                column: "date_planted",
                value: new DateTime(2023, 12, 4, 13, 49, 22, 568, DateTimeKind.Local).AddTicks(1579));

            migrationBuilder.UpdateData(
                table: "GardenPlants",
                keyColumns: new[] { "garden_id", "plant_id" },
                keyValues: new object[] { 1, 7 },
                column: "date_planted",
                value: new DateTime(2023, 12, 4, 13, 49, 22, 568, DateTimeKind.Local).AddTicks(1580));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "bOPFnRPqQB6Cb1z34z2O8g==");

            migrationBuilder.CreateIndex(
                name: "IX_Users_garden_id",
                table: "Users",
                column: "garden_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Gardens_garden_id",
                table: "Users",
                column: "garden_id",
                principalTable: "Gardens",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
