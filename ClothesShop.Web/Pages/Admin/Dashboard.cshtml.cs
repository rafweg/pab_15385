using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothesShop.Web.Pages.Admin;

public class DashboardModel : PageModel
{
    // Dashboard summary properties
    public int TotalProducts { get; set; }
    public int TotalOrders { get; set; }
    public int PendingOrders { get; set; }
    public int LowStockItems { get; set; }

    // Data lists for display tables
    public List<Order> RecentOrders { get; set; }
    public List<Product> LowStockProducts { get; set; }

    public void OnGet()
    {
        // Replace with real data access logic
        TotalProducts = 120;
        TotalOrders = 350;
        PendingOrders = 17;
        LowStockItems = 8;

        RecentOrders = new List<Order>
        {
            new() { Id = 101, CustomerName = "Alice Johnson", TotalAmount = 199.99m, Status = "Pending" },
            new() { Id = 102, CustomerName = "Bob Smith", TotalAmount = 89.50m, Status = "Shipped" },
            new() { Id = 103, CustomerName = "Catherine West", TotalAmount = 240.75m, Status = "Processing" }
        };

        LowStockProducts = new List<Product>
        {
            new() { Id = 1, Name = "Classic T-Shirt", Category = "Men", StockQuantity = 2 },
            new() { Id = 2, Name = "Denim Jeans", Category = "Women", StockQuantity = 5 },
            new() { Id = 3, Name = "Summer Dress", Category = "Women", StockQuantity = 1 }
        };
    }
}

// Supporting classes for demo purposes
public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int StockQuantity { get; set; }
}