using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class addnewtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPANY",
                schema: "SCT",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RUC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyFiscalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "COMPANY_TYPE",
                schema: "SCT",
                columns: table => new
                {
                    CompanyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY_TYPE", x => x.CompanyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "USER_COMPANY",
                schema: "SCT",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_COMPANY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_COMPANY_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_COMPANY_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "SCT",
                        principalTable: "COMPANY",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMPANY_COMPANY_TYPE",
                schema: "SCT",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY_COMPANY_TYPE", x => new { x.CompanyId, x.CompanyTypeId });
                    table.ForeignKey(
                        name: "FK_COMPANY_COMPANY_TYPE_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "SCT",
                        principalTable: "COMPANY",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMPANY_COMPANY_TYPE_COMPANY_TYPE_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalSchema: "SCT",
                        principalTable: "COMPANY_TYPE",
                        principalColumn: "CompanyTypeId",
                        onDelete: ReferentialAction.Cascade);
                });  

            migrationBuilder.CreateIndex(
                name: "IX_COMPANY_COMPANY_TYPE_CompanyTypeId",
                schema: "SCT",
                table: "COMPANY_COMPANY_TYPE",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_COMPANY_CompanyId",
                schema: "SCT",
                table: "USER_COMPANY",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMPANY_COMPANY_TYPE",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "USER_COMPANY",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "COMPANY_TYPE",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "COMPANY",
                schema: "SCT");
        }
    }
}
