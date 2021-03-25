using System;
using Microsoft.EntityFrameworkCore.Migrations;
using SpaceParkModel.Database;

namespace SpaceParkModel.Migrations
{
    public partial class SpaceparkTesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Occupancies",
                columns: new[] { "ID", "ArrivalTime", "DepartureTime", "ParkingSpotID", "PersonID", "SpaceshipID" },
                values: new object[,]
                {
                    { 10, new DateTime(2021, 3, 24, 12, 22, 6, 27, DateTimeKind.Unspecified).AddTicks(7378), null, 1, 2, 4 },
                    { 6, new DateTime(2021, 3, 22, 19, 41, 28, 504, DateTimeKind.Unspecified).AddTicks(2097), new DateTime(2021, 3, 22, 19, 41, 30, 457, DateTimeKind.Unspecified).AddTicks(3860), 1, 1, 1 },
                    { 5, new DateTime(2021, 3, 22, 19, 39, 46, 359, DateTimeKind.Unspecified).AddTicks(6936), new DateTime(2021, 3, 22, 19, 40, 6, 198, DateTimeKind.Unspecified).AddTicks(9431), 1, 1, 1 },
                    { 4, new DateTime(2021, 3, 22, 19, 39, 23, 343, DateTimeKind.Unspecified).AddTicks(3465), new DateTime(2021, 3, 22, 19, 39, 27, 365, DateTimeKind.Unspecified).AddTicks(8201), 1, 1, 1 },
                    { 3, new DateTime(2021, 3, 22, 19, 35, 45, 711, DateTimeKind.Unspecified).AddTicks(9285), new DateTime(2021, 3, 22, 19, 38, 27, 669, DateTimeKind.Unspecified).AddTicks(1688), 1, 1, 1 },
                    { 2, new DateTime(2021, 3, 22, 19, 25, 37, 861, DateTimeKind.Unspecified).AddTicks(3124), new DateTime(2021, 3, 22, 19, 35, 9, 153, DateTimeKind.Unspecified).AddTicks(2960), 1, 1, 2 },
                    { 1, new DateTime(2021, 3, 22, 18, 26, 53, 649, DateTimeKind.Unspecified).AddTicks(1508), new DateTime(2021, 3, 22, 19, 8, 0, 370, DateTimeKind.Unspecified).AddTicks(1198), 1, 1, 2 },
                    { 7, new DateTime(2021, 3, 23, 11, 42, 13, 452, DateTimeKind.Unspecified).AddTicks(6063), new DateTime(2021, 3, 23, 11, 42, 58, 902, DateTimeKind.Unspecified).AddTicks(6162), 1, 1, 1 },
                    { 9, new DateTime(2021, 3, 24, 12, 17, 55, 996, DateTimeKind.Unspecified).AddTicks(8849), new DateTime(2021, 3, 24, 12, 18, 2, 489, DateTimeKind.Unspecified).AddTicks(5235), 1, 2, 3 },
                    { 12, new DateTime(2021, 3, 25, 10, 1, 58, 653, DateTimeKind.Unspecified).AddTicks(3626), new DateTime(2021, 3, 25, 10, 59, 42, 120, DateTimeKind.Unspecified).AddTicks(8154), 2, 1, 4 },
                    { 13, new DateTime(2021, 3, 25, 10, 59, 56, 40, DateTimeKind.Unspecified).AddTicks(3378), new DateTime(2021, 3, 25, 11, 0, 7, 53, DateTimeKind.Unspecified).AddTicks(8153), 1, 1, 6 },
                    { 14, new DateTime(2021, 3, 24, 12, 22, 6, 27, DateTimeKind.Unspecified).AddTicks(7378), null, 3, 1, 1 },
                    { 15, new DateTime(2021, 3, 24, 12, 22, 6, 27, DateTimeKind.Unspecified).AddTicks(7378), null, 4, 3, 2 },
                    { 16, new DateTime(2021, 3, 24, 12, 22, 6, 27, DateTimeKind.Unspecified).AddTicks(7378), null, 5, 4, 3 },
                    { 17, new DateTime(2021, 3, 24, 12, 22, 6, 27, DateTimeKind.Unspecified).AddTicks(7378), null, 6, 5, 5 },
                    { 11, new DateTime(2021, 3, 24, 12, 22, 24, 582, DateTimeKind.Unspecified).AddTicks(8001), new DateTime(2021, 3, 24, 14, 43, 18, 801, DateTimeKind.Unspecified).AddTicks(4517), 4, 1, 5 },
                    { 8, new DateTime(2021, 3, 24, 12, 15, 59, 46, DateTimeKind.Unspecified).AddTicks(1350), new DateTime(2021, 3, 24, 12, 16, 12, 736, DateTimeKind.Unspecified).AddTicks(1570), 1, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSizes",
                columns: new[] { "ID", "Price", "Size" },
                values: new object[,]
                {
                    { 1, 100.00m, 1000 },
                    { 3, 5000.00m, 150000 },
                    { 2, 1000.00m, 10000 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSpots",
                columns: new[] { "Spot", "ParkingSizeID" },
                values: new object[,]
                {
                    { 5, 2 },
                    { 4, 2 },
                    { 3, 1 },
                    { 2, 1 },
                    { 1, 1 },
                    { 6, 3 }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 2, "R2-D2" },
                    { 1, "Luke Skywalker" },
                    { 3, "Leia Organa" },
                    { 4, "C-3PO" },
                    { 5, "Beru Whitesun lars" }
                });

            migrationBuilder.InsertData(
                table: "Spaceships",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 4, "Slave 1" },
                    { 3, "Republic Cruiser" },
                    { 5, "Republic attack cruiser" },
                    { 1, "X-wing" },
                    { 2, "Imperial shuttle" },
                    { 6, "J-type diplomatic barge" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Occupancies",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ParkingSizes",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkingSizes",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkingSizes",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Spot",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Spot",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Spot",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Spot",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Spot",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Spot",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Spaceships",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Spaceships",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Spaceships",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Spaceships",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Spaceships",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Spaceships",
                keyColumn: "ID",
                keyValue: 6);
        }
    }
}
