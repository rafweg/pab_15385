using System.Security.Claims;
using ClothesShop.Application.Services;
using ClothesShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothesShop.Web.Pages.User;

[Authorize(Roles = "User")]
public class DashboardModel : PageModel
{
    private readonly IOrderService _orderService;

    public DashboardModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public int TotalOrders { get; set; }
    public int PendingOrders { get; set; }
    public int CompletedOrders { get; set; }
    public List<Order> RecentOrders { get; set; } = new();

    public async Task OnGetAsync()
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
        var orders = await _orderService.GetOrdersByUserIdAsync(userEmail);

        TotalOrders = orders.Count();
        PendingOrders = orders.Count(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Processing);
        CompletedOrders = orders.Count(o => o.Status == OrderStatus.Delivered);

        RecentOrders = orders.OrderByDescending(o => o.CreatedAt).Take(5).ToList();
    }
}