using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasKey(o => o.Id);
        
        modelBuilder.Entity<Order>().Property(o => o.CustomerName).HasMaxLength(100).IsRequired();
        
        modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}