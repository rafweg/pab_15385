using ClothesShop.Application.Interfaces;
using ClothesShop.Application.Services;
using ClothesShop.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClothesShop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}