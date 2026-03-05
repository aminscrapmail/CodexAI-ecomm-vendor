using EcommVendor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommVendor.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Category).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(1000);
            entity.Property(p => p.Price).HasPrecision(18, 2);
            entity.Property(p => p.ModifiedBy).IsRequired().HasMaxLength(80);
        });
    }
}
