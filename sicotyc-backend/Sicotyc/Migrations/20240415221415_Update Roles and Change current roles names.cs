using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolesandChangecurrentrolesnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {                     
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {                    
                    { "9a908a4d-646b-40e6-bde7-8f6abf13e10c", null, "Forwarder-Biller", "FORWARDER-BILLER" },
                    { "a6c791d7-81ef-4ab9-a708-5cb722d68dcd", null, "Forwarder-Coordinator", "FORWARDER-COORDINATOR" }                    
                });            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            
        }
    }
}
