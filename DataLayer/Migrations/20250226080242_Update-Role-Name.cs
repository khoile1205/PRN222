using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "01e50e7a-44af-4750-b99f-0e900e4bf2c6");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "55dfb8e1-7a33-444e-9cf2-e165a58e43b1");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "627f72b1-2592-4947-9da1-b23b37d325c1");

            migrationBuilder.DeleteData(
                table: "BeverageSizes",
                keyColumn: "Id",
                keyValue: "d7931b86-1b40-43e1-8585-0d3cb9246ee8");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b2519e4d-69c6-4e9d-97b9-1cdd415a635e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "fd3958ce-cc9c-449e-9b26-c22f0f6ac43f");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "02f25958-9a19-494f-99da-54892269c217");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "1f944201-16a3-4484-bdcc-0ff91532fad3");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "97e28946-bc38-4c1f-bf57-941825a90571");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "ac9d3b2d-a05f-48fd-b9ff-ca99815f8a8d");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: "b74c84e8-b6cc-4112-8135-be54f1862bb0");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "01e50e7a-44af-4750-b99f-0e900e4bf2c6", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4439), null, "M", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4439) },
                    { "55dfb8e1-7a33-444e-9cf2-e165a58e43b1", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4425), null, "S", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4425) },
                    { "627f72b1-2592-4947-9da1-b23b37d325c1", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4441), null, "L", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4441) },
                    { "d7931b86-1b40-43e1-8585-0d3cb9246ee8", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4445), null, "XL", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4445) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { "b2519e4d-69c6-4e9d-97b9-1cdd415a635e", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4466), null, "Admin", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4467) },
                    { "fd3958ce-cc9c-449e-9b26-c22f0f6ac43f", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4468), null, "Staff", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4468) }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "EndTime", "ShiftCode", "ShiftType", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { "02f25958-9a19-494f-99da-54892269c217", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4294), null, "Evening Part-time Shift", new TimeSpan(0, 22, 0, 0, 0), 2, 0, new TimeSpan(0, 17, 0, 0, 0), new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4294) },
                    { "1f944201-16a3-4484-bdcc-0ff91532fad3", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4291), null, "Afternoon Part-time Shift", new TimeSpan(0, 17, 0, 0, 0), 1, 0, new TimeSpan(0, 12, 0, 0, 0), new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4292) },
                    { "97e28946-bc38-4c1f-bf57-941825a90571", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4296), null, "Morning Full-time Shift", new TimeSpan(0, 14, 0, 0, 0), 0, 1, new TimeSpan(0, 7, 0, 0, 0), new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4296) },
                    { "ac9d3b2d-a05f-48fd-b9ff-ca99815f8a8d", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4298), null, "Afternoon Full-time Shift", new TimeSpan(0, 22, 0, 0, 0), 1, 1, new TimeSpan(0, 14, 0, 0, 0), new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4298) },
                    { "b74c84e8-b6cc-4112-8135-be54f1862bb0", new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4281), null, "Morning Part-time Shift", new TimeSpan(0, 12, 0, 0, 0), 0, 0, new TimeSpan(0, 7, 0, 0, 0), new DateTime(2025, 1, 20, 18, 25, 12, 235, DateTimeKind.Utc).AddTicks(4284) }
                });
        }
    }
}
