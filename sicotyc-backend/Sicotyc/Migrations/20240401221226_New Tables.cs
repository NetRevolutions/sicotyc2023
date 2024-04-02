using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class NewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.CreateTable(
                name: "DRIVER",
                schema: "SCT",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenceClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnableIMO = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AntecedentePolicialesExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AntecedentesPenalesExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DRIVER", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "TRANSPORT_DETAIL",
                schema: "SCT",
                columns: table => new
                {
                    TransportDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Large = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Axles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSPORT_DETAIL", x => x.TransportDetailId);
                });

            migrationBuilder.CreateTable(
                name: "WHAREHOUSE",
                schema: "SCT",
                columns: table => new
                {
                    WhareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AliasName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHAREHOUSE", x => x.WhareHouseId);
                    table.ForeignKey(
                        name: "FK_WHAREHOUSE_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "SCT",
                        principalTable: "COMPANY",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WHAREHOUSE_DRIVER_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "SCT",
                        principalTable: "DRIVER",
                        principalColumn: "DriverId");
                });

            migrationBuilder.CreateTable(
                name: "COMPLEMENT_TRANSPORT",
                schema: "SCT",
                columns: table => new
                {
                    ComplementTransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricationYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModelYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TechnicalReviewExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditinalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleQualificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleQualificationExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleConfiguration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPLEMENT_TRANSPORT", x => x.ComplementTransportId);
                    table.ForeignKey(
                        name: "FK_COMPLEMENT_TRANSPORT_TRANSPORT_DETAIL_TransportDetailId",
                        column: x => x.TransportDetailId,
                        principalSchema: "SCT",
                        principalTable: "TRANSPORT_DETAIL",
                        principalColumn: "TransportDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UNIT_TRANSPORT",
                schema: "SCT",
                columns: table => new
                {
                    UnitTransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricationYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModelYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoatExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TechnicalReviewExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fuel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditinalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleQualificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleQualificationExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleConfiguration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNIT_TRANSPORT", x => x.UnitTransportId);
                    table.ForeignKey(
                        name: "FK_UNIT_TRANSPORT_TRANSPORT_DETAIL_TransportDetailId",
                        column: x => x.TransportDetailId,
                        principalSchema: "SCT",
                        principalTable: "TRANSPORT_DETAIL",
                        principalColumn: "TransportDetailId",
                        onDelete: ReferentialAction.Cascade);
                });            

            migrationBuilder.CreateIndex(
                name: "IX_COMPLEMENT_TRANSPORT_TransportDetailId",
                schema: "SCT",
                table: "COMPLEMENT_TRANSPORT",
                column: "TransportDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UNIT_TRANSPORT_TransportDetailId",
                schema: "SCT",
                table: "UNIT_TRANSPORT",
                column: "TransportDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_WHAREHOUSE_CompanyId",
                schema: "SCT",
                table: "WHAREHOUSE",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WHAREHOUSE_DriverId",
                schema: "SCT",
                table: "WHAREHOUSE",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMPLEMENT_TRANSPORT",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "UNIT_TRANSPORT",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "WHAREHOUSE",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "TRANSPORT_DETAIL",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "DRIVER",
                schema: "SCT");
            
        }
    }
}
