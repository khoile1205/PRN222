using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initialize_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeverageCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeverageSizes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShiftCode = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatQuantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beverages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beverages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beverages_BeverageCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BeverageCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_InventoryCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "InventoryCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableDetails_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeverageDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeverageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeverageDetails_BeverageSizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "BeverageSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeverageDetails_Beverages_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryUpdateHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InventoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Spending = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryUpdateHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryUpdateHistories_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShiftDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftStaff_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftStaff_Users_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoucherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_TableDetails_TableDetailId",
                        column: x => x.TableDetailId,
                        principalTable: "TableDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableBeverages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeverageDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableBeverages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableBeverages_BeverageDetails_BeverageDetailId",
                        column: x => x.BeverageDetailId,
                        principalTable: "BeverageDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableBeverages_TableDetails_TableDetailId",
                        column: x => x.TableDetailId,
                        principalTable: "TableDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BeverageDetails_BeverageId",
                table: "BeverageDetails",
                column: "BeverageId");

            migrationBuilder.CreateIndex(
                name: "IX_BeverageDetails_SizeId",
                table: "BeverageDetails",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Beverages_CategoryId",
                table: "Beverages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CategoryId",
                table: "Inventories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryUpdateHistories_InventoryId",
                table: "InventoryUpdateHistories",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftStaff_ShiftId",
                table: "ShiftStaff",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftStaff_StaffId",
                table: "ShiftStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TableBeverages_BeverageDetailId",
                table: "TableBeverages",
                column: "BeverageDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TableBeverages_TableDetailId",
                table: "TableBeverages",
                column: "TableDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TableDetails_TableId",
                table: "TableDetails",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TableDetailId",
                table: "Transactions",
                column: "TableDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_VoucherId",
                table: "Transactions",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryUpdateHistories");

            migrationBuilder.DropTable(
                name: "ShiftStaff");

            migrationBuilder.DropTable(
                name: "TableBeverages");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BeverageDetails");

            migrationBuilder.DropTable(
                name: "TableDetails");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "InventoryCategories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "BeverageSizes");

            migrationBuilder.DropTable(
                name: "Beverages");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "BeverageCategories");
        }
    }
}
