﻿// <auto-generated />
using System;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250226080242_Update-Role-Name")]
    partial class UpdateRoleName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataLayer.Entities.Beverage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Beverages");
                });

            modelBuilder.Entity("DataLayer.Entities.BeverageCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BeverageCategories");
                });

            modelBuilder.Entity("DataLayer.Entities.BeverageDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BeverageId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SizeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BeverageId");

                    b.HasIndex("SizeId");

                    b.ToTable("BeverageDetails");
                });

            modelBuilder.Entity("DataLayer.Entities.BeverageSize", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BeverageSizes");

                    b.HasData(
                        new
                        {
                            Id = "a502461d-a6d0-4370-9fd9-235a1e401831",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3571),
                            SizeName = "S",
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3571)
                        },
                        new
                        {
                            Id = "a1102fba-2058-47ce-910c-cd3dae037674",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3585),
                            SizeName = "M",
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3585)
                        },
                        new
                        {
                            Id = "e1902526-c8df-4f88-ba2c-42e438e10bb7",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3587),
                            SizeName = "L",
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3587)
                        },
                        new
                        {
                            Id = "22ffa634-3f57-4b75-8a3a-843bc23bb4b9",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3591),
                            SizeName = "XL",
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3592)
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Inventory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("DataLayer.Entities.InventoryCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("InventoryCategories");
                });

            modelBuilder.Entity("DataLayer.Entities.InventoryUpdateHistory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("InventoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Spending")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("InventoryUpdateHistories");
                });

            modelBuilder.Entity("DataLayer.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "d653fcbc-2833-4a6e-a52e-5b8258cbbc0b",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3989),
                            RoleName = "Admin",
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3989)
                        },
                        new
                        {
                            Id = "13c0bcc9-a5d3-4725-862d-e5955c642ed4",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3992),
                            RoleName = "Staff",
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3992)
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Shift", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("ShiftCode")
                        .HasColumnType("int");

                    b.Property<int>("ShiftType")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Shifts");

                    b.HasData(
                        new
                        {
                            Id = "650a47de-391b-4058-af99-191ff0ddfef4",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3432),
                            Description = "Morning Part-time Shift",
                            EndTime = new TimeSpan(0, 12, 0, 0, 0),
                            ShiftCode = 0,
                            ShiftType = 0,
                            StartTime = new TimeSpan(0, 7, 0, 0, 0),
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3435)
                        },
                        new
                        {
                            Id = "3fba7926-0d85-453b-b893-e27d51daf176",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3443),
                            Description = "Afternoon Part-time Shift",
                            EndTime = new TimeSpan(0, 17, 0, 0, 0),
                            ShiftCode = 1,
                            ShiftType = 0,
                            StartTime = new TimeSpan(0, 12, 0, 0, 0),
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3443)
                        },
                        new
                        {
                            Id = "3431bc6e-8c06-43c8-86b7-3166630b76b8",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3446),
                            Description = "Evening Part-time Shift",
                            EndTime = new TimeSpan(0, 22, 0, 0, 0),
                            ShiftCode = 2,
                            ShiftType = 0,
                            StartTime = new TimeSpan(0, 17, 0, 0, 0),
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3446)
                        },
                        new
                        {
                            Id = "2432c8cc-a029-4be2-a2c6-5a42430b5c0c",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3448),
                            Description = "Morning Full-time Shift",
                            EndTime = new TimeSpan(0, 14, 0, 0, 0),
                            ShiftCode = 0,
                            ShiftType = 1,
                            StartTime = new TimeSpan(0, 7, 0, 0, 0),
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3448)
                        },
                        new
                        {
                            Id = "1d4f299d-042c-4c57-8df1-8516a91c48fc",
                            CreatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3450),
                            Description = "Afternoon Full-time Shift",
                            EndTime = new TimeSpan(0, 22, 0, 0, 0),
                            ShiftCode = 1,
                            ShiftType = 1,
                            StartTime = new TimeSpan(0, 14, 0, 0, 0),
                            UpdatedAt = new DateTime(2025, 2, 26, 8, 2, 40, 490, DateTimeKind.Utc).AddTicks(3450)
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.ShiftStaff", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ShiftDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShiftId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StaffId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ShiftId");

                    b.HasIndex("StaffId");

                    b.ToTable("ShiftStaff");
                });

            modelBuilder.Entity("DataLayer.Entities.Table", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("SeatQuantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("DataLayer.Entities.TableBeverage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BeverageDetailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("TableDetailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BeverageDetailId");

                    b.HasIndex("TableDetailId");

                    b.ToTable("TableBeverages");
                });

            modelBuilder.Entity("DataLayer.Entities.TableDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("TableId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("TableDetails");
                });

            modelBuilder.Entity("DataLayer.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TableDetailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("VoucherId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TableDetailId")
                        .IsUnique();

                    b.HasIndex("VoucherId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("DataLayer.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataLayer.Entities.Voucher", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MaxDiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("DataLayer.Entities.Beverage", b =>
                {
                    b.HasOne("DataLayer.Entities.BeverageCategory", "BeverageCategory")
                        .WithMany("Beverages")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BeverageCategory");
                });

            modelBuilder.Entity("DataLayer.Entities.BeverageDetail", b =>
                {
                    b.HasOne("DataLayer.Entities.Beverage", "Beverage")
                        .WithMany("BeverageDetails")
                        .HasForeignKey("BeverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.BeverageSize", "Size")
                        .WithMany("BeverageDetails")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beverage");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("DataLayer.Entities.Inventory", b =>
                {
                    b.HasOne("DataLayer.Entities.InventoryCategory", "InventoryCategory")
                        .WithMany("Inventories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryCategory");
                });

            modelBuilder.Entity("DataLayer.Entities.InventoryUpdateHistory", b =>
                {
                    b.HasOne("DataLayer.Entities.Inventory", "Inventory")
                        .WithMany("InventoryUpdateHistory")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("DataLayer.Entities.ShiftStaff", b =>
                {
                    b.HasOne("DataLayer.Entities.Shift", "Shift")
                        .WithMany("ShiftStaff")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.User", "Staff")
                        .WithMany("ShiftStaff")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shift");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("DataLayer.Entities.TableBeverage", b =>
                {
                    b.HasOne("DataLayer.Entities.BeverageDetail", "BeverageDetail")
                        .WithMany()
                        .HasForeignKey("BeverageDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.TableDetail", "TableDetail")
                        .WithMany("TableBeverages")
                        .HasForeignKey("TableDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BeverageDetail");

                    b.Navigation("TableDetail");
                });

            modelBuilder.Entity("DataLayer.Entities.TableDetail", b =>
                {
                    b.HasOne("DataLayer.Entities.Table", "Table")
                        .WithMany("TableDetails")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");
                });

            modelBuilder.Entity("DataLayer.Entities.Transaction", b =>
                {
                    b.HasOne("DataLayer.Entities.TableDetail", "TableDetail")
                        .WithOne("Transaction")
                        .HasForeignKey("DataLayer.Entities.Transaction", "TableDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.Voucher", "Voucher")
                        .WithMany("Transactions")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TableDetail");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("DataLayer.Entities.User", b =>
                {
                    b.HasOne("DataLayer.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DataLayer.Entities.Beverage", b =>
                {
                    b.Navigation("BeverageDetails");
                });

            modelBuilder.Entity("DataLayer.Entities.BeverageCategory", b =>
                {
                    b.Navigation("Beverages");
                });

            modelBuilder.Entity("DataLayer.Entities.BeverageSize", b =>
                {
                    b.Navigation("BeverageDetails");
                });

            modelBuilder.Entity("DataLayer.Entities.Inventory", b =>
                {
                    b.Navigation("InventoryUpdateHistory");
                });

            modelBuilder.Entity("DataLayer.Entities.InventoryCategory", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("DataLayer.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DataLayer.Entities.Shift", b =>
                {
                    b.Navigation("ShiftStaff");
                });

            modelBuilder.Entity("DataLayer.Entities.Table", b =>
                {
                    b.Navigation("TableDetails");
                });

            modelBuilder.Entity("DataLayer.Entities.TableDetail", b =>
                {
                    b.Navigation("TableBeverages");

                    b.Navigation("Transaction")
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Entities.User", b =>
                {
                    b.Navigation("ShiftStaff");
                });

            modelBuilder.Entity("DataLayer.Entities.Voucher", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
