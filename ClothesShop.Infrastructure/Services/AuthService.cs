using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClothesShop.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ClothesShop.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly string _audience;
    private readonly string _issuer;
    private readonly string _secretKey;

    public AuthService(string secretKey, string issuer, string audience)
    {
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = GetDemoUser(email, password);
        if (user == null) return null;

        return GenerateJwtToken(user.Email, user.Role, user.FullName);
    }

    public async Task<bool> RegisterAsync(string email, string password, string fullName, string role = "User")
    {
        await Task.CompletedTask;
        return true;
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private string GenerateJwtToken(string email, string role, string fullName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim("FullName", fullName)
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private DemoUser? GetDemoUser(string email, string password)
    {
        var demoUsers = new List<DemoUser>
        {
            new() { Email = "admin@clothesshop.com", Password = "Admin123!", Role = "Admin", FullName = "Admin User" },
            new() { Email = "user@clothesshop.com", Password = "User123!", Role = "User", FullName = "Regular User" }
        };

        return demoUsers.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    private class DemoUser
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}