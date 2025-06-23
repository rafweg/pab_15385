namespace ClothesShop.Application.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(string email, string password);
    Task<bool> RegisterAsync(string email, string password, string fullName, string role = "User");
    Task<bool> ValidateTokenAsync(string token);
}