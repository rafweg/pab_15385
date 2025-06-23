#nullable disable
using System.Text.Json;
using ClothesShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}

public class ClothesShopDbContext : DbContext
{
    public ClothesShopDbContext(DbContextOptions<ClothesShopDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

            entity.Property(e => e.Sizes)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!) ?? new List<string>()
                );

            entity.Property(e => e.Colors)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!) ?? new List<string>()
                );
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");

            entity.HasOne(e => e.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(e => e.ProductId);

            entity.HasOne(e => e.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(e => e.OrderId);
        });

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Classic Cotton T-Shirt",
                Description = "Comfortable cotton t-shirt perfect for everyday wear",
                Price = 29.99m,
                Category = "T-Shirts",
                Brand = "BasicWear",
                Sizes = new List<string> { "XS", "S", "M", "L", "XL" },
                Colors = new List<string> { "White", "Black", "Navy" },
                ImageUrl = "/images/tshirt1.jpg",
                StockQuantity = 100,
                IsActive = true
            },
            new Product
            {
                Id = 2,
                Name = "Denim Jacket",
                Description = "Classic denim jacket with modern fit",
                Price = 89.99m,
                Category = "Jackets",
                Brand = "DenimWorks",
                Sizes = new List<string> { "S", "M", "L", "XL" },
                Colors = new List<string> { "Blue", "Black" },
                ImageUrl = "/images/jacket1.jpg",
                StockQuantity = 100,
                IsActive = true
            },
            new Product
            {
                Id = 3,
                Name = "Summer Dress",
                Description = "Light and airy summer dress",
                Price = 59.99m,
                Category = "Dresses",
                Brand = "SunnyStyle",
                Sizes = new List<string> { "XS", "S", "M", "L" },
                Colors = new List<string> { "Floral", "Yellow" },
                ImageUrl = "/images/dress1.jpg",
                StockQuantity = 75,
                IsActive = true
            }
        );
    }
}