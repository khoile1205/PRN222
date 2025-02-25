using System;
using System.Collections.Generic;
using Caffeine.Models;
using Microsoft.EntityFrameworkCore;

namespace Caffeine.Data;

public partial class CaffeineContext : DbContext
{
    public CaffeineContext()
    {
    }

    public CaffeineContext(DbContextOptions<CaffeineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beverage> Beverages { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<CafeTable> CafeTables { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Revenue> Revenues { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beverage>(entity =>
        {
            entity.HasKey(e => e.BeverageId).HasName("PK__Beverage__344F53E9BF412B5E");

            entity.ToTable("Beverage");

            entity.Property(e => e.BeverageId)
                .ValueGeneratedNever()
                .HasColumnName("BeverageID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Bill__11F2FC4A017B4EF0");

            entity.ToTable("Bill");

            entity.Property(e => e.BillId)
                .ValueGeneratedNever()
                .HasColumnName("BillID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.TableId).HasColumnName("TableID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Staff).WithMany(p => p.Bills)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Bill__StaffID__2A4B4B5E");

            entity.HasOne(d => d.Table).WithMany(p => p.Bills)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("FK__Bill__TableID__2B3F6F97");
        });

        modelBuilder.Entity<CafeTable>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK__CafeTabl__7D5F018E83416EC4");

            entity.ToTable("CafeTable");

            entity.Property(e => e.TableId)
                .ValueGeneratedNever()
                .HasColumnName("TableID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D3ABC0A658");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId)
                .ValueGeneratedNever()
                .HasColumnName("InventoryID");
            entity.Property(e => e.BeverageId).HasColumnName("BeverageID");

            entity.HasOne(d => d.Beverage).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.BeverageId)
                .HasConstraintName("FK__Inventory__Bever__300424B4");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42F2FC0FACDFD");

            entity.ToTable("Promotion");

            entity.Property(e => e.PromotionId)
                .ValueGeneratedNever()
                .HasColumnName("PromotionID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Revenue>(entity =>
        {
            entity.HasKey(e => e.RevenueId).HasName("PK__Revenue__275F173DA5009C04");

            entity.ToTable("Revenue");

            entity.Property(e => e.RevenueId)
                .ValueGeneratedNever()
                .HasColumnName("RevenueID");
            entity.Property(e => e.TotalRevenue).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7076C3370");

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasColumnName("StaffID");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Profile).HasColumnType("text");
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Shift)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
