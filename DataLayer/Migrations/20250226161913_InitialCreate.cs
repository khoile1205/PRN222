using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "22ffa634-3f57-4b75-8a3a-843bc23bb4b9");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "a1102fba-2058-47ce-910c-cd3dae037674");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "a502461d-a6d0-4370-9fd9-235a1e401831");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "e1902526-c8df-4f88-ba2c-42e438e10bb7");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "13c0bcc9-a5d3-4725-862d-e5955c642ed4");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d653fcbc-2833-4a6e-a52e-5b8258cbbc0b");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "1d4f299d-042c-4c57-8df1-8516a91c48fc");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "2432c8cc-a029-4be2-a2c6-5a42430b5c0c");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "3431bc6e-8c06-43c8-86b7-3166630b76b8");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "3fba7926-0d85-453b-b893-e27d51daf176");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "650a47de-391b-4058-af99-191ff0ddfef4");

            migrationBuilder.InsertData(
                table: "BeverageSizes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "SizeName", "UpdatedAt" },
                values: new object[,]
                {
                    { "460d6706-37f6-4857-b1bd-ef191fcdc203", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7661), null, "XL", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7661) },
                    { "56fb3734-3163-4a31-91ce-f13cf6603e7b", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7656), null, "L", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7656) },
                    { "a48053c8-7ae1-4b89-a77e-ce1712cc8fc3", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7642), null, "S", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7642) },
                    { "cc5f922e-2baf-4f4a-b5a9-61bb89602403", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7653), null, "M", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7654) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { "494a415a-4c97-4191-afc4-8ff55bf402fd", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(8127), null, "Admin", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(8128) },
                    { "7ec32894-f95c-4588-962e-c0cf011dea09", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(8131), null, "Staff", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(8131) }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "EndTime", "ShiftCode", "ShiftType", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { "2492b2c7-4af2-428a-84a4-978b3a9f788c", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7443), null, "Morning Full-time Shift", new TimeSpan(0, 14, 0, 0, 0), 0, 1, new TimeSpan(0, 7, 0, 0, 0), new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7444) },
                    { "8639d53c-101a-4177-9e8c-d1dade2163c3", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7438), null, "Afternoon Part-time Shift", new TimeSpan(0, 17, 0, 0, 0), 1, 0, new TimeSpan(0, 12, 0, 0, 0), new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7438) },
                    { "8c7902d4-c868-4214-87b4-f4581c28e37a", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7446), null, "Afternoon Full-time Shift", new TimeSpan(0, 22, 0, 0, 0), 1, 1, new TimeSpan(0, 14, 0, 0, 0), new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7446) },
                    { "a4ec5d6c-7077-41c4-8e06-8b5e8a842ee9", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7429), null, "Morning Part-time Shift", new TimeSpan(0, 12, 0, 0, 0), 0, 0, new TimeSpan(0, 7, 0, 0, 0), new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7431) },
                    { "d020b995-c436-49e0-ba79-de5726ec0fa6", new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7441), null, "Evening Part-time Shift", new TimeSpan(0, 22, 0, 0, 0), 2, 0, new TimeSpan(0, 17, 0, 0, 0), new DateTime(2025, 2, 26, 16, 19, 11, 631, DateTimeKind.Utc).AddTicks(7441) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "460d6706-37f6-4857-b1bd-ef191fcdc203");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "56fb3734-3163-4a31-91ce-f13cf6603e7b");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "a48053c8-7ae1-4b89-a77e-ce1712cc8fc3");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "cc5f922e-2baf-4f4a-b5a9-61bb89602403");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "494a415a-4c97-4191-afc4-8ff55bf402fd");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7ec32894-f95c-4588-962e-c0cf011dea09");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "2492b2c7-4af2-428a-84a4-978b3a9f788c");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "8639d53c-101a-4177-9e8c-d1dade2163c3");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "8c7902d4-c868-4214-87b4-f4581c28e37a");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "a4ec5d6c-7077-41c4-8e06-8b5e8a842ee9");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "d020b995-c436-49e0-ba79-de5726ec0fa6");

            migrationBuilder.InsertData(
                table: "BeverageSizes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "SizeName", "UpdatedAt" },
                values: new object[,]
                {
                    { "22ffa634-3f57-4b75-8a3a-843bc23bb4b9", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3591), null, "XL", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3592) },
                    { "a1102fba-2058-47ce-910c-cd3dae037674", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3585), null, "M", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3585) },
                    { "a502461d-a6d0-4370-9fd9-235a1e401831", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3571), null, "S", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3571) },
                    { "e1902526-c8df-4f88-ba2c-42e438e10bb7", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3587), null, "L", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3587) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { "13c0bcc9-a5d3-4725-862d-e5955c642ed4", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3992), null, "Staff", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3992) },
                    { "d653fcbc-2833-4a6e-a52e-5b8258cbbc0b", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3989), null, "Admin", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3989) }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "EndTime", "ShiftCode", "ShiftType", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { "1d4f299d-042c-4c57-8df1-8516a91c48fc", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3450), null, "Afternoon Full-time Shift", new TimeSpan(0, 22, 0, 0, 0), 1, 1, new TimeSpan(0, 14, 0, 0, 0), new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3450) },
                    { "2432c8cc-a029-4be2-a2c6-5a42430b5c0c", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3448), null, "Morning Full-time Shift", new TimeSpan(0, 14, 0, 0, 0), 0, 1, new TimeSpan(0, 7, 0, 0, 0), new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3448) },
                    { "3431bc6e-8c06-43c8-86b7-3166630b76b8", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3446), null, "Evening Part-time Shift", new TimeSpan(0, 22, 0, 0, 0), 2, 0, new TimeSpan(0, 17, 0, 0, 0), new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3446) },
                    { "3fba7926-0d85-453b-b893-e27d51daf176", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3443), null, "Afternoon Part-time Shift", new TimeSpan(0, 17, 0, 0, 0), 1, 0, new TimeSpan(0, 12, 0, 0, 0), new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3443) },
                    { "650a47de-391b-4058-af99-191ff0ddfef4", new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3432), null, "Morning Part-time Shift", new TimeSpan(0, 12, 0, 0, 0), 0, 0, new TimeSpan(0, 7, 0, 0, 0), new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3435) }
                });
        }
    }
}
