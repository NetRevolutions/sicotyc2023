using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDriverTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenceClass",
                schema: "SCT",
                table: "DRIVER");

            migrationBuilder.RenameColumn(
                name: "LicenseCategory",
                schema: "SCT",
                table: "DRIVER",
                newName: "LicenseType");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.RenameColumn(
                name: "LicenseType",
                schema: "SCT",
                table: "DRIVER",
                newName: "LicenseCategory");

            migrationBuilder.AddColumn<string>(
                name: "LicenceClass",
                schema: "SCT",
                table: "DRIVER",
                type: "nvarchar(max)",
                nullable: true);            
        }
    }
}
