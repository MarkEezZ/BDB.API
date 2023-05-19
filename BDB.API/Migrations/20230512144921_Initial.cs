using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BDB.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Detectors",
                columns: table => new
                {
                    DetectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlacementAdress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detectors", x => x.DetectorId);
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OverSpeed = table.Column<double>(type: "float", nullable: false),
                    FineAmount = table.Column<double>(type: "float", nullable: false),
                    CarNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineId);
                    table.ForeignKey(
                        name: "FK_Fines_Detectors_DetectorId",
                        column: x => x.DetectorId,
                        principalTable: "Detectors",
                        principalColumn: "DetectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Detectors",
                columns: new[] { "DetectorId", "PlacementAdress" },
                values: new object[,]
                {
                    { 1, "Nezavisimosty, 176" },
                    { 2, "Surganova, 22" },
                    { 3, "Kolasa, 55" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fines_DetectorId",
                table: "Fines",
                column: "DetectorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "Detectors");
        }
    }
}
