using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [MaxLength(500)] public string Description { get; set; } = string.Empty;

    [Required] public decimal Price { get; set; }

    [Required] [MaxLength(50)] public string Category { get; set; } = string.Empty;

    [MaxLength(50)] public string Brand { get; set; } = string.Empty;

    public List<string> Sizes { get; set; } = new();
    public List<string> Colors { get; set; } = new();

    [MaxLength(200)] public string ImageUrl { get; set; } = string.Empty;

    public int StockQuantity { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}