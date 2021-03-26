using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkModel.Migrations
{
    public partial class PaymenttableAlongWithSeedingForIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OccupancyID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "ID", "Amount", "OccupancyID" },
                values: new object[,]
                {
                    { 1, 600.00m, 1 },
                    { 2, 100.00m, 2 },
                    { 3, 400.00m, 3 },
                    { 4, 10000.00m, 4 },
                    { 5, 850.00m, 5 },
                    { 6, 80.00m, 6 },
                    { 7, 1200.00m, 7 },
                    { 8, 250.00m, 8 },
                    { 9, 400.00m, 9 },
                    { 10, 700.00m, 11 },
                    { 11, 350.00m, 12 },
                    { 12, 300.00m, 13 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
