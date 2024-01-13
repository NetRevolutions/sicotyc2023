using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldsUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ac6a0ce-6e77-4f7e-a6b1-cdd8a48a726b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69848c4f-42af-4b8d-9ddf-6b69998dc28d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da783011-878e-4ed2-8ceb-7e4ef63193f9");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34a3be24-087e-4296-bef7-428872e0f99d", null, "Manager", "MANAGER" },
                    { "b6b6a75d-86f3-431f-9548-7f64aab0f28c", null, "Member", "MEMBER" },
                    { "dc05c348-c103-4552-ae72-cd1a80552c43", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("1a011e51-2471-4ccd-174c-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(582));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("23078793-cd0a-4718-2aa4-08da71da4714"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(584));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("2d253e01-afa1-4a59-bc6a-26526f0d8498"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(589));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("47b84a27-c75a-44d3-174d-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(586));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("58250d62-975a-4883-81f7-946c91cf2dec"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(605));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("5f38d3fd-f34e-45eb-aebf-512f5ebd94ee"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(1546));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("6963984f-c5e0-4ed9-9647-46ac7054e344"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(607));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("792f255c-2b8b-42e6-9968-2855373e5c86"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(596));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("7e603067-a1ed-4b52-174b-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(579));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("867c1549-7132-4e8e-174a-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(574));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8bd83659-b611-488d-aaac-e5d418bac06c"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(1559));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8dc0180a-2ffc-4807-803a-37aab6ecaab2"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(591));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("b2a7d680-b5dc-41d1-9792-695602fc2954"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(598));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("de0cc597-ad66-4497-acab-33617eb077bd"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(594));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e5c70df3-cf54-477f-881d-7d142f0b51aa"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(1556));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e83581fc-e05c-4c80-b5c2-e381fd7765d7"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(613));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("eaf628ee-9413-472e-a5b7-3c9d45f10cf0"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(603));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fdc11a23-1dc7-4160-bb9d-019579c56e46"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(1553));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fe8b2536-5a20-4680-8dfe-526000df87e1"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(248));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(267));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(271));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(269));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34a3be24-087e-4296-bef7-428872e0f99d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6b6a75d-86f3-431f-9548-7f64aab0f28c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc05c348-c103-4552-ae72-cd1a80552c43");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ac6a0ce-6e77-4f7e-a6b1-cdd8a48a726b", null, "Member", "MEMBER" },
                    { "69848c4f-42af-4b8d-9ddf-6b69998dc28d", null, "Administrator", "ADMINISTRATOR" },
                    { "da783011-878e-4ed2-8ceb-7e4ef63193f9", null, "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("1a011e51-2471-4ccd-174c-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6010));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("23078793-cd0a-4718-2aa4-08da71da4714"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6012));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("2d253e01-afa1-4a59-bc6a-26526f0d8498"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("47b84a27-c75a-44d3-174d-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6018));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("58250d62-975a-4883-81f7-946c91cf2dec"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6036));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("5f38d3fd-f34e-45eb-aebf-512f5ebd94ee"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6044));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("6963984f-c5e0-4ed9-9647-46ac7054e344"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6038));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("792f255c-2b8b-42e6-9968-2855373e5c86"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6027));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("7e603067-a1ed-4b52-174b-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6007));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("867c1549-7132-4e8e-174a-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6003));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8bd83659-b611-488d-aaac-e5d418bac06c"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6052));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8dc0180a-2ffc-4807-803a-37aab6ecaab2"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6022));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("b2a7d680-b5dc-41d1-9792-695602fc2954"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6029));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("de0cc597-ad66-4497-acab-33617eb077bd"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6024));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e5c70df3-cf54-477f-881d-7d142f0b51aa"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6049));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e83581fc-e05c-4c80-b5c2-e381fd7765d7"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6040));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("eaf628ee-9413-472e-a5b7-3c9d45f10cf0"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6033));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fdc11a23-1dc7-4160-bb9d-019579c56e46"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6047));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fe8b2536-5a20-4680-8dfe-526000df87e1"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(6031));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(5690));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(5712));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(5715));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"),
                column: "CreatedOn",
                value: new DateTime(2023, 12, 26, 4, 51, 25, 863, DateTimeKind.Utc).AddTicks(5714));
        }
    }
}
