using Microsoft.EntityFrameworkCore;
using QTKD.Api.Entities;

namespace QTKD.Api.Data;

public class QtkdDbContext : DbContext
{
    public QtkdDbContext(DbContextOptions<QtkdDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<PriceList> PriceLists => Set<PriceList>();
    public DbSet<PriceItem> PriceItems => Set<PriceItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Quan hệ Contract (n) - Customer (1).
        // Restrict: không cho xóa Customer khi còn hợp đồng tham chiếu tới.
        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Customer)
            .WithMany(cu => cu.Contracts)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>().HasIndex(c => c.ContractNo).IsUnique();

        // Lưu enum dạng chữ ("Draft") để đọc trực tiếp trong PostgreSQL.
        modelBuilder.Entity<Contract>().Property(c => c.Status).HasConversion<string>();

        // Quan hệ PriceList (1) - PriceItem (n).
        modelBuilder.Entity<PriceItem>()
            .HasOne(i => i.PriceList)
            .WithMany(p => p.Items)
            .HasForeignKey(i => i.PriceListId);

        modelBuilder.Entity<PriceItem>().Property(i => i.UnitPrice).HasPrecision(18, 2);

        // Dữ liệu mẫu (bước 5): nằm trong migration nên máy nào chạy database update cũng có.
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "Công ty Minh Phát", Status = "Active" },
            new Customer { Id = 2, Name = "Công ty An Khang", Status = "Active" });

        modelBuilder.Entity<Contract>().HasData(
            new Contract
            {
                Id = 1,
                ContractNo = "HD-2026-018",
                CustomerId = 1,
                StartDate = new DateOnly(2026, 12, 1),
                EndDate = new DateOnly(2027, 11, 30),
                Status = ContractStatus.Draft
            });

        modelBuilder.Entity<PriceList>().HasData(
            new PriceList { Id = 1, Name = "Bảng giá 2026", EffectiveDate = new DateOnly(2026, 1, 1) });

        modelBuilder.Entity<PriceItem>().HasData(
            new PriceItem { Id = 1, PriceListId = 1, ServiceCode = "SVC-01", UnitPrice = 1500000m },
            new PriceItem { Id = 2, PriceListId = 1, ServiceCode = "SVC-02", UnitPrice = 2500000m });
    }
}
