namespace ClothesShop.Application.DTOs;

public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
}

public class AuthResponseDto
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string? FullName { get; set; }
    public string? Role { get; set; }
    public DateTime ExpiresAt { get; set; }
}