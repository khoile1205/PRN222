using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace DataLayer.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftStaff> ShiftStaff { get; set; }
        public DbSet<InventoryCategory> InventoryCategories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryUpdateHistory> InventoryUpdateHistories { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<BeverageCategory> BeverageCategories { get; set; }
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<BeverageSize> BeverageSizes { get; set; }
        public DbSet<BeverageDetail> BeverageDetails { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableDetail> TableDetails { get; set; }
        public DbSet<TableBeverage> TableBeverages { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shift>().HasData(
                new Shift
                {
                    ShiftCode = Enums.ShiftCode.Morning,
                    ShiftType = Enums.ShiftType.PartTime,
                    Description = "Morning Part-time Shift",
                    StartTime = new TimeSpan(7, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0)
                },
                new Shift
                {
                    ShiftCode = Enums.ShiftCode.Afternoon,
                    ShiftType = Enums.ShiftType.PartTime,
                    Description = "Afternoon Part-time Shift",
                    StartTime = new TimeSpan(12, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0)
                },
                new Shift
                {
                    ShiftCode = Enums.ShiftCode.Evening,
                    ShiftType = Enums.ShiftType.PartTime,
                    Description = "Evening Part-time Shift",
                    StartTime = new TimeSpan(17, 0, 0),
                    EndTime = new TimeSpan(22, 0, 0)
                },
                new Shift
                {
                    ShiftCode = Enums.ShiftCode.Morning,
                    ShiftType = Enums.ShiftType.FullTime,
                    Description = "Morning Full-time Shift",
                    StartTime = new TimeSpan(7, 0, 0),
                    EndTime = new TimeSpan(14, 0, 0)
                },
                new Shift
                {
                    ShiftCode = Enums.ShiftCode.Afternoon,
                    ShiftType = Enums.ShiftType.FullTime,
                    Description = "Afternoon Full-time Shift",
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(22, 0, 0)
                }
            );

            modelBuilder.Entity<BeverageSize>().HasData(
                new BeverageSize
                {
                    SizeName = Enums.BeverageSize.S.ToString()
                },
                new BeverageSize
                {
                    SizeName = Enums.BeverageSize.M.ToString()
                },
                new BeverageSize
                {
                    SizeName = Enums.BeverageSize.L.ToString()
                },
                new BeverageSize
                {
                    SizeName = Enums.BeverageSize.XL.ToString()
                }
           );

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .HasConversion<string>();

            modelBuilder.Entity<Role>()
                .HasData(
                new Role
                {
                    RoleName = RoleEnum.Admin
                },
                new Role
                {
                    RoleName = RoleEnum.Staff
                }
          );

            // 1-1 relation
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TableDetail)
                .WithOne(td => td.Transaction)
                .HasForeignKey<Transaction>(t => t.TableDetailId);

            // 1-N relation
            modelBuilder.Entity<ShiftStaff>()
                .HasOne(ss => ss.Staff)
                .WithMany(s => s.ShiftStaff)
                .HasForeignKey(ss => ss.StaffId);

            modelBuilder.Entity<ShiftStaff>()
                .HasOne(ss => ss.Shift)
                .WithMany(s => s.ShiftStaff)
                .HasForeignKey(ss => ss.ShiftId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Table relation
            modelBuilder.Entity<TableDetail>()
                .HasOne(td => td.Table)
                .WithMany(t => t.TableDetails)
                .HasForeignKey(td => td.TableId);

            modelBuilder.Entity<TableBeverage>()
                .HasOne(tb => tb.TableDetail)
                .WithMany(td => td.TableBeverages)
                .HasForeignKey(tb => tb.TableDetailId);

            // Transaction relation
            modelBuilder.Entity<Transaction>()
               .HasOne(t => t.Voucher)
               .WithMany(v => v.Transactions)
               .HasForeignKey(t => t.VoucherId);

            // Beverage relation
            modelBuilder.Entity<BeverageDetail>()
                .HasOne(bd => bd.Beverage)
                .WithMany(b => b.BeverageDetails)
                .HasForeignKey(bd => bd.BeverageId);

            modelBuilder.Entity<BeverageDetail>()
                .HasOne(bd => bd.Size)
                .WithMany(s => s.BeverageDetails)
                .HasForeignKey(bd => bd.SizeId);

            modelBuilder.Entity<Beverage>()
                .HasOne(b => b.BeverageCategory)
                .WithMany(c => c.Beverages)
                .HasForeignKey(b => b.CategoryId);

            // Inventory relation
            modelBuilder.Entity<InventoryCategory>()
                .HasMany(ic => ic.Inventories)
                .WithOne(i => i.InventoryCategory)
                .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<Inventory>()
                .HasMany(i => i.InventoryUpdateHistory)
                .WithOne(iuh => iuh.Inventory)
                .HasForeignKey(i => i.InventoryId);
        }
    }
}
