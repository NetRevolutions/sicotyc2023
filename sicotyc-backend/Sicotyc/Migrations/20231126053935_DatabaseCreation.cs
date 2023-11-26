using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SCT");

            migrationBuilder.CreateTable(
                name: "LOOKUP_CODE_GROUP",
                schema: "SCT",
                columns: table => new
                {
                    LookupCodeGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LookupCodeGroupName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOOKUP_CODE_GROUP", x => x.LookupCodeGroupId);
                });

            migrationBuilder.CreateTable(
                name: "LOOKUP_CODE",
                schema: "SCT",
                columns: table => new
                {
                    LookupCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LookupCodeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LookupCodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LookupCodeOrder = table.Column<int>(type: "int", nullable: false),
                    LookupCodeGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOOKUP_CODE", x => x.LookupCodeId);
                    table.ForeignKey(
                        name: "FK_LOOKUP_CODE_LOOKUP_CODE_GROUP_LookupCodeGroupId",
                        column: x => x.LookupCodeGroupId,
                        principalSchema: "SCT",
                        principalTable: "LOOKUP_CODE_GROUP",
                        principalColumn: "LookupCodeGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LOOKUP_CODE_LookupCodeGroupId",
                schema: "SCT",
                table: "LOOKUP_CODE",
                column: "LookupCodeGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOOKUP_CODE",
                schema: "SCT");

            migrationBuilder.DropTable(
                name: "LOOKUP_CODE_GROUP",
                schema: "SCT");
        }
    }
}
