using ClothesShop.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothesShop.Web.Pages.Admin;

[Authorize(Roles = "Admin")]
public class ProductsModel : PageModel
{
    private readonly IProductService _productService;

    public ProductsModel(IProductService productService)
    {
        _productService = productService;
    }

    public List<Domain.Entities.Product> Products { get; set; } = new();

    public async Task OnGetAsync()
    {
        var products = await _productService.GetAllAsync();
        Products = products.ToList();
    }
}