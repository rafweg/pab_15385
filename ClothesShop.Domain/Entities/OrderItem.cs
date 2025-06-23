using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }

    [Required] public int OrderId { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public int Quantity { get; set; }

    [Required] public decimal UnitPrice { get; set; }

    [MaxLength(20)] public string Size { get; set; } = string.Empty;

    [MaxLength(30)] public string Color { get; set; } = string.Empty;

    public decimal TotalPrice => Quantity * UnitPrice;

    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}