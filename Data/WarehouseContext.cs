using Microsoft.EntityFrameworkCore;
using safonenko.Models;

namespace safonenko.Data;

public class WarehouseContext(DbContextOptions<WarehouseContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<StorageLocation> StorageLocations => Set<StorageLocation>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Article)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StorageLocation>()
            .HasIndex(l => new { l.Row, l.Shelf, l.Cell })
            .IsUnique();

        modelBuilder.Entity<StockMovement>()
            .HasOne(m => m.Product)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(m => m.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StockMovement>()
            .HasOne(m => m.Supplier)
            .WithMany(s => s.StockMovements)
            .HasForeignKey(m => m.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StockMovement>()
            .HasOne(m => m.StorageLocation)
            .WithMany(l => l.StockMovements)
            .HasForeignKey(m => m.StorageLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
