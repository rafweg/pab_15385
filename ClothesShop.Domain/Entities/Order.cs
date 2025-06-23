using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    [Required] public string UserId { get; set; } = string.Empty;

    [Required] [MaxLength(100)] public string CustomerName { get; set; } = string.Empty;

    [Required] [EmailAddress] public string CustomerEmail { get; set; } = string.Empty;

    [Required] [MaxLength(200)] public string ShippingAddress { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4
}