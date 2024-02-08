using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class InsertClaimValidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d2cf2f6-b070-4918-8fbb-43c7831f956e", null, "Member", "MEMBER" },
                    { "9785127f-682e-4284-b173-1796d3a1e619", null, "Manager", "MANAGER" },
                    { "c8aad812-5b2e-4f8c-9287-9749c269de6e", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("1a011e51-2471-4ccd-174c-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2277));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("23078793-cd0a-4718-2aa4-08da71da4714"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2279));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("2d253e01-afa1-4a59-bc6a-26526f0d8498"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2285));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("47b84a27-c75a-44d3-174d-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2282));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("58250d62-975a-4883-81f7-946c91cf2dec"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2332));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("5f38d3fd-f34e-45eb-aebf-512f5ebd94ee"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2339));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("6963984f-c5e0-4ed9-9647-46ac7054e344"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2335));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("792f255c-2b8b-42e6-9968-2855373e5c86"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2291));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("7e603067-a1ed-4b52-174b-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2274));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("867c1549-7132-4e8e-174a-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2268));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8bd83659-b611-488d-aaac-e5d418bac06c"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2346));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8dc0180a-2ffc-4807-803a-37aab6ecaab2"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2287));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("b2a7d680-b5dc-41d1-9792-695602fc2954"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2293));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("de0cc597-ad66-4497-acab-33617eb077bd"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2289));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e5c70df3-cf54-477f-881d-7d142f0b51aa"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2343));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e83581fc-e05c-4c80-b5c2-e381fd7765d7"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2337));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("eaf628ee-9413-472e-a5b7-3c9d45f10cf0"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2298));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fdc11a23-1dc7-4160-bb9d-019579c56e46"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2341));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fe8b2536-5a20-4680-8dfe-526000df87e1"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2296));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(1944));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(1961));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(1965));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(1963));

            migrationBuilder.InsertData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                columns: new[] { "LookupCodeGroupId", "CreatedOn", "CreatedBy", "DeletedOn", "LookupCodeGroupName", "LastUpdatedOn", "UpdatedBy" },
                values: new object[] { new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"), new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(1966), "SYSTEM", null, "CLAIMS PERMITIDOS", null, null });

            migrationBuilder.InsertData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                columns: new[] { "LookupCodeId", "CreatedOn", "CreatedBy", "DeletedOn", "LookupCodeGroupId", "LookupCodeName", "LookupCodeOrder", "LookupCodeValue", "LastUpdatedOn", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1aec098a-859a-4586-80b6-b6f4beb848fb"), new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2357), "SYSTEM", null, new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"), "ID", 5, "Id", null, null },
                    { new Guid("37a294bb-d8e2-4655-80a8-a2fe719766d4"), new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2350), "SYSTEM", null, new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"), "FIRSTNAME", 2, "FirstName", null, null },
                    { new Guid("50bd3490-2377-4945-9229-f018f6b07bf8"), new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2359), "SYSTEM", null, new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"), "PHONENUMBER", 6, "PhoneNumber", null, null },
                    { new Guid("6b1b516f-9073-4657-8a4c-9cb7ebe8ea25"), new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2354), "SYSTEM", null, new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"), "EMAIL", 4, "Email", null, null },
                    { new Guid("752ce625-da67-4842-b19d-18c5572dbbce"), new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2348), "SYSTEM", null, new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"), "USERNAME", 1, "UserName", null, null },
                    { new Guid("e129c250-de59-45d3-8794-58e073ff8064"), new DateTime(2024, 1, 26, 4, 36, 43, 330, DateTimeKind.Utc).AddTicks(2352), "SYSTEM", null, new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"), "LASTNAME", 3, "LastName", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d2cf2f6-b070-4918-8fbb-43c7831f956e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9785127f-682e-4284-b173-1796d3a1e619");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8aad812-5b2e-4f8c-9287-9749c269de6e");

            migrationBuilder.DeleteData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("1aec098a-859a-4586-80b6-b6f4beb848fb"));

            migrationBuilder.DeleteData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("37a294bb-d8e2-4655-80a8-a2fe719766d4"));

            migrationBuilder.DeleteData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("50bd3490-2377-4945-9229-f018f6b07bf8"));

            migrationBuilder.DeleteData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("6b1b516f-9073-4657-8a4c-9cb7ebe8ea25"));

            migrationBuilder.DeleteData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("752ce625-da67-4842-b19d-18c5572dbbce"));

            migrationBuilder.DeleteData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e129c250-de59-45d3-8794-58e073ff8064"));

            migrationBuilder.DeleteData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"));

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
    }
}
