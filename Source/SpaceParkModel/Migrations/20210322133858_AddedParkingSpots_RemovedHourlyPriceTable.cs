using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkModel.Migrations
{
    public partial class AddedParkingSpots_RemovedHourlyPriceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceBreakPoints");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Spaceships");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "Occupancies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpotID",
                table: "Occupancies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParkingSizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSizes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpots",
                columns: table => new
                {
                    Spot = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingSizeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpots", x => x.Spot);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSizes");

            migrationBuilder.DropTable(
                name: "ParkingSpots");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Occupancies");

            migrationBuilder.DropColumn(
                name: "ParkingSpotID",
                table: "Occupancies");

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Spaceships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PriceBreakPoints",
                columns: table => new
                {
                    Hours = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceBreakPoints", x => x.Hours);
                });
        }
    }
}
