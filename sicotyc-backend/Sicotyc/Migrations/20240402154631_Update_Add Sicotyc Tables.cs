using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class Update_AddSicotycTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WHAREHOUSE_DRIVER_DriverId",
                schema: "SCT",
                table: "WHAREHOUSE");

            migrationBuilder.DropIndex(
                name: "IX_WHAREHOUSE_DriverId",
                schema: "SCT",
                table: "WHAREHOUSE");

            migrationBuilder.DropColumn(
                name: "DriverId",
                schema: "SCT",
                table: "WHAREHOUSE");

            migrationBuilder.DropColumn(
                name: "LicenseExpiration",
                schema: "SCT",
                table: "DRIVER");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                schema: "SCT",
                table: "DRIVER");

            migrationBuilder.DropColumn(
                name: "LicenseType",
                schema: "SCT",
                table: "DRIVER");

            migrationBuilder.CreateTable(
                name: "DRIVER_LICENSE",
                schema: "SCT",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LicenseType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LicenseExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DRIVER_LICENSE", x => new { x.DriverId, x.LicenseNumber, x.LicenseType });
                    table.ForeignKey(
                        name: "FK_DRIVER_LICENSE_DRIVER_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "SCT",
                        principalTable: "DRIVER",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DRIVER_WHAREHOUSE",
                schema: "SCT",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DRIVER_WHAREHOUSE", x => new { x.DriverId, x.WhareHouseId });
                    table.ForeignKey(
                        name: "FK_DRIVER_WHAREHOUSE_DRIVER_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "SCT",
                        principalTable: "DRIVER",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DRIVER_WHAREHOUSE_WHAREHOUSE_WhareHouseId",
                        column: x => x.WhareHouseId,
                        principalSchema: "SCT",
                        principalTable: "WHAREHOUSE",
                        principalColumn: "WhareHouseId",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateIndex(
                name: "IX_DRIVER_WHAREHOUSE_WhareHouseId",
                schema: "SCT",
                table: "DRIVER_WHAREHOUSE",
                column: "WhareHouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DRIVER_LICENSE",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "DRIVER_WHAREHOUSE",
                schema: "SCT");           

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                schema: "SCT",
                table: "WHAREHOUSE",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenseExpiration",
                schema: "SCT",
                table: "DRIVER",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                schema: "SCT",
                table: "DRIVER",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseType",
                schema: "SCT",
                table: "DRIVER",
                type: "nvarchar(max)",
                nullable: true);
            
            migrationBuilder.CreateIndex(
                name: "IX_WHAREHOUSE_DriverId",
                schema: "SCT",
                table: "WHAREHOUSE",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_WHAREHOUSE_DRIVER_DriverId",
                schema: "SCT",
                table: "WHAREHOUSE",
                column: "DriverId",
                principalSchema: "SCT",
                principalTable: "DRIVER",
                principalColumn: "DriverId");
        }
    }
}
