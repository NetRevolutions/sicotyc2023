using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
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
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
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
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                columns: new[] { "LookupCodeGroupId", "CreatedOn", "CreatedBy", "DeletedOn", "LookupCodeGroupName", "LastUpdatedOn", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"), new DateTime(2023, 11, 29, 5, 40, 7, 351, DateTimeKind.Utc).AddTicks(9930), "SYSTEM", null, "TIPO DE PAGO PEAJE", null, null },
                    { new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"), new DateTime(2023, 11, 29, 5, 40, 7, 351, DateTimeKind.Utc).AddTicks(9939), "SYSTEM", null, "TIPO DE DOC. IDENTIDAD", null, null },
                    { new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"), new DateTime(2023, 11, 29, 5, 40, 7, 351, DateTimeKind.Utc).AddTicks(9944), "SYSTEM", null, "TIPO DE SERVICIO", null, null },
                    { new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"), new DateTime(2023, 11, 29, 5, 40, 7, 351, DateTimeKind.Utc).AddTicks(9942), "SYSTEM", null, "TIPO DE EMPRESA", null, null }
                });

            migrationBuilder.InsertData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                columns: new[] { "LookupCodeId", "CreatedOn", "CreatedBy", "DeletedOn", "LookupCodeGroupId", "LookupCodeName", "LookupCodeOrder", "LookupCodeValue", "LastUpdatedOn", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1a011e51-2471-4ccd-174c-08da70ae983a"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(34), "SYSTEM", null, new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"), "Por Eje3", 3, "ByAxis3", null, null },
                    { new Guid("23078793-cd0a-4718-2aa4-08da71da4714"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(36), "SYSTEM", null, new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"), "Por Eje4", 4, "ByAxis4", null, null },
                    { new Guid("2d253e01-afa1-4a59-bc6a-26526f0d8498"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(39), "SYSTEM", null, new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"), "Documento Nacional de Identidad", 1, "DNI", null, null },
                    { new Guid("47b84a27-c75a-44d3-174d-08da70ae983a"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(38), "SYSTEM", null, new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"), "Por Eje5", 5, "ByAxis5", null, null },
                    { new Guid("58250d62-975a-4883-81f7-946c91cf2dec"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(51), "SYSTEM", null, new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"), "Otros", 2, "OT", null, null },
                    { new Guid("5f38d3fd-f34e-45eb-aebf-512f5ebd94ee"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(80), "SYSTEM", null, new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"), "CARGA SUELTA", 3, "CS", null, null },
                    { new Guid("6963984f-c5e0-4ed9-9647-46ac7054e344"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(52), "SYSTEM", null, new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"), "IMPORTACION", 1, "IMPO", null, null },
                    { new Guid("792f255c-2b8b-42e6-9968-2855373e5c86"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(45), "SYSTEM", null, new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"), "Partida de Nacimiento", 4, "PNAC", null, null },
                    { new Guid("7e603067-a1ed-4b52-174b-08da70ae983a"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(32), "SYSTEM", null, new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"), "Por Eje2", 2, "ByAxis2", null, null },
                    { new Guid("867c1549-7132-4e8e-174a-08da70ae983a"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(28), "SYSTEM", null, new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"), "Por Eje", 1, "ByAxis", null, null },
                    { new Guid("8bd83659-b611-488d-aaac-e5d418bac06c"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(85), "SYSTEM", null, new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"), "CAMA BAJA", 6, "CB", null, null },
                    { new Guid("8dc0180a-2ffc-4807-803a-37aab6ecaab2"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(41), "SYSTEM", null, new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"), "Carnet de Extranjería", 2, "CEX", null, null },
                    { new Guid("b2a7d680-b5dc-41d1-9792-695602fc2954"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(47), "SYSTEM", null, new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"), "Carnet de FFAA", 5, "CFFAA", null, null },
                    { new Guid("de0cc597-ad66-4497-acab-33617eb077bd"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(44), "SYSTEM", null, new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"), "Pasaporte", 3, "PASS", null, null },
                    { new Guid("e5c70df3-cf54-477f-881d-7d142f0b51aa"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(83), "SYSTEM", null, new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"), "TRACCIÓN", 5, "TX", null, null },
                    { new Guid("e83581fc-e05c-4c80-b5c2-e381fd7765d7"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(53), "SYSTEM", null, new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"), "EXPORTACION", 2, "EXPO", null, null },
                    { new Guid("eaf628ee-9413-472e-a5b7-3c9d45f10cf0"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(50), "SYSTEM", null, new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"), "Empresa de Transporte", 1, "ET", null, null },
                    { new Guid("fdc11a23-1dc7-4160-bb9d-019579c56e46"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(81), "SYSTEM", null, new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"), "DEVOLUCIÓN DE VACÍO", 4, "DV", null, null },
                    { new Guid("fe8b2536-5a20-4680-8dfe-526000df87e1"), new DateTime(2023, 11, 29, 5, 40, 7, 352, DateTimeKind.Utc).AddTicks(48), "SYSTEM", null, new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"), "Pasaporte Diplomatico", 6, "PASSD", null, null }
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
