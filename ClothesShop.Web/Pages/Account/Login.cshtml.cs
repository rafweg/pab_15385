using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ClothesShop.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothesShop.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IAuthService _authService;

    public LoginModel(IAuthService authService)
    {
        _authService = authService;
    }

    [BindProperty] public InputModel Input { get; set; }

    public string? ReturnUrl { get; set; }

    public async Task OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;

        if (!ModelState.IsValid) return Page();

        var token = await _authService.LoginAsync(Input.Email, Input.Password);

        if (token != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, Input.Email),
                new("Token", token)
            };

            var role = Input.Email.Contains("admin") ? "Admin" : "User";
            claims.Add(new Claim(ClaimTypes.Role, role));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = Input.RememberMe
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);

            return RedirectToPage(role == "Admin" ? "/Admin/Dashboard" : "/User/Dashboard");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }

    public class InputModel
    {
        [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
    }
}