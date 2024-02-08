using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicotyc.Migrations
{
    /// <inheritdoc />
    public partial class AddImgClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04f37cc1-f402-4c03-8ce7-abfb989113e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f350c0e-a937-46ef-ad08-3df3d8f3a374");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac97d47f-10b6-461f-a51a-2935c5c55f4b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6d8172c3-09d4-45da-87f9-c0e7329ed482", null, "Administrator", "ADMINISTRATOR" },
                    { "702e7f29-f3dc-447d-8352-6012bd645926", null, "Member", "MEMBER" },
                    { "c370de86-d7f7-47be-bf5e-77bc3c416c93", null, "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("1a011e51-2471-4ccd-174c-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7576));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("1aec098a-859a-4586-80b6-b6f4beb848fb"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7661));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("23078793-cd0a-4718-2aa4-08da71da4714"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7579));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("2d253e01-afa1-4a59-bc6a-26526f0d8498"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("37a294bb-d8e2-4655-80a8-a2fe719766d4"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7653));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("47b84a27-c75a-44d3-174d-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7581));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("50bd3490-2377-4945-9229-f018f6b07bf8"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7664));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("58250d62-975a-4883-81f7-946c91cf2dec"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7603));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("5f38d3fd-f34e-45eb-aebf-512f5ebd94ee"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7641));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("6963984f-c5e0-4ed9-9647-46ac7054e344"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7605));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("6b1b516f-9073-4657-8a4c-9cb7ebe8ea25"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7659));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("752ce625-da67-4842-b19d-18c5572dbbce"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7650));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("792f255c-2b8b-42e6-9968-2855373e5c86"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7594));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("7e603067-a1ed-4b52-174b-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7574));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("867c1549-7132-4e8e-174a-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7568));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8bd83659-b611-488d-aaac-e5d418bac06c"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7648));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8dc0180a-2ffc-4807-803a-37aab6ecaab2"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7590));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("b2a7d680-b5dc-41d1-9792-695602fc2954"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("de0cc597-ad66-4497-acab-33617eb077bd"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7592));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e129c250-de59-45d3-8794-58e073ff8064"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7655));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e5c70df3-cf54-477f-881d-7d142f0b51aa"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7646));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e83581fc-e05c-4c80-b5c2-e381fd7765d7"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7638));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("eaf628ee-9413-472e-a5b7-3c9d45f10cf0"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("f7ab3cf1-afe9-4b2b-977f-953d9f3b9275"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7666));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fdc11a23-1dc7-4160-bb9d-019579c56e46"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7643));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fe8b2536-5a20-4680-8dfe-526000df87e1"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7599));

            migrationBuilder.InsertData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                columns: new[] { "LookupCodeId", "CreatedOn", "CreatedBy", "DeletedOn", "LookupCodeGroupId", "LookupCodeName", "LookupCodeOrder", "LookupCodeValue", "LastUpdatedOn", "UpdatedBy" },
                values: new object[] { new Guid("8e009c6d-5920-4135-8a26-49ec04c6e7d5"), new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7668), "SYSTEM", null, new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"), "IMG", 7, "Img", null, null });

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7253));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7273));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7280));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"),
                column: "CreatedOn",
                value: new DateTime(2024, 2, 7, 16, 19, 39, 615, DateTimeKind.Utc).AddTicks(7272));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d8172c3-09d4-45da-87f9-c0e7329ed482");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "702e7f29-f3dc-447d-8352-6012bd645926");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c370de86-d7f7-47be-bf5e-77bc3c416c93");

            migrationBuilder.DeleteData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8e009c6d-5920-4135-8a26-49ec04c6e7d5"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04f37cc1-f402-4c03-8ce7-abfb989113e8", null, "Administrator", "ADMINISTRATOR" },
                    { "2f350c0e-a937-46ef-ad08-3df3d8f3a374", null, "Manager", "MANAGER" },
                    { "ac97d47f-10b6-461f-a51a-2935c5c55f4b", null, "Member", "MEMBER" }
                });

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("1a011e51-2471-4ccd-174c-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3154));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("1aec098a-859a-4586-80b6-b6f4beb848fb"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3202));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("23078793-cd0a-4718-2aa4-08da71da4714"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3156));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("2d253e01-afa1-4a59-bc6a-26526f0d8498"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3160));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("37a294bb-d8e2-4655-80a8-a2fe719766d4"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3188));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("47b84a27-c75a-44d3-174d-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3158));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("50bd3490-2377-4945-9229-f018f6b07bf8"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3224));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("58250d62-975a-4883-81f7-946c91cf2dec"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3173));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("5f38d3fd-f34e-45eb-aebf-512f5ebd94ee"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3179));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("6963984f-c5e0-4ed9-9647-46ac7054e344"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3175));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("6b1b516f-9073-4657-8a4c-9cb7ebe8ea25"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3192));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("752ce625-da67-4842-b19d-18c5572dbbce"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3186));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("792f255c-2b8b-42e6-9968-2855373e5c86"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3165));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("7e603067-a1ed-4b52-174b-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3151));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("867c1549-7132-4e8e-174a-08da70ae983a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3147));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8bd83659-b611-488d-aaac-e5d418bac06c"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3185));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("8dc0180a-2ffc-4807-803a-37aab6ecaab2"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3162));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("b2a7d680-b5dc-41d1-9792-695602fc2954"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3167));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("de0cc597-ad66-4497-acab-33617eb077bd"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3164));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e129c250-de59-45d3-8794-58e073ff8064"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3190));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e5c70df3-cf54-477f-881d-7d142f0b51aa"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3183));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("e83581fc-e05c-4c80-b5c2-e381fd7765d7"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3177));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("eaf628ee-9413-472e-a5b7-3c9d45f10cf0"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3171));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("f7ab3cf1-afe9-4b2b-977f-953d9f3b9275"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3226));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fdc11a23-1dc7-4160-bb9d-019579c56e46"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3180));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE",
                keyColumn: "LookupCodeId",
                keyValue: new Guid("fe8b2536-5a20-4680-8dfe-526000df87e1"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(3169));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(2881));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(2901));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(2903));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("cda56e87-1b44-4625-9f19-ac7eb282a9b7"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(2906));

            migrationBuilder.UpdateData(
                schema: "SCT",
                table: "LOOKUP_CODE_GROUP",
                keyColumn: "LookupCodeGroupId",
                keyValue: new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 26, 16, 31, 40, 827, DateTimeKind.Utc).AddTicks(2902));
        }
    }
}
