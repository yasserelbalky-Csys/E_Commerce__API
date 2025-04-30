using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.UserViewModel;
using E_Commerce_MVC.Models.UtilitesSupport;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace E_Commerce_MVC.Controllers;

public class AccountController : Controller
{
    private readonly AccountApiService _accountService;
    private readonly HttpClient _httpClient;

    public AccountController(HttpClient httpClient, AccountApiService accountService)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5097/api/User/"); // API base URL
        _accountService = accountService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try {
            // Send login request to the API
            var result = await _accountService.Login(model);
            Console.WriteLine($"resut is: {result.ToJson()}");

            if (result != null) {
                HttpContext.Session.SetString("JWTToken", result?.Token!);

                // Decode the token and get user roles
                var roles = _accountService.GetRolesFromToken(result?.Token!);
                _accountService.GetUserIdFromToken(result?.Token!);

                // sign in the user
                var singinResult = await SinginUser(result!, roles);
                var claims = User.Claims.ToList();

                // after login, check roles
                if (roles.Contains("Admin")) {
                    return RedirectToAction("Index", "Dashboard");
                } else {
                    return RedirectToAction("Index", "Home");
                }
            }
        } catch (Exception ex) {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
        }

        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try {
            // Send registration request to the API
            var result = await _accountService.Register(model);

            if (result.IsSuccessStatusCode)
                // Registration successful, redirect to login page
                return RedirectToAction("Login");

            // Directly deserialize the response content into the ErrorResponse object
            var errorResponse = await result.Content.ReadFromJsonAsync<ErrorResponse>();

            if (errorResponse != null) {
                var errorMessage =
                    $"{(string.IsNullOrWhiteSpace(errorResponse.Title) ? "Error" : errorResponse.Title.Trim('.'))}: {string.Join(", ", errorResponse.Errors.SelectMany(e => e.Value))}";
                ModelState.AddModelError(string.Empty, errorMessage);
            } else {
                ModelState.AddModelError(string.Empty, "An unknown error occurred.");
            }

            #region Unused error logging

            //var errorObject = await result.Content.ReadFromJsonAsync<object>();
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"Error Object: {errorObject}");
            //Console.ResetColor();
            //// Cast the object to JsonElement to access its properties
            //if (errorObject is JsonElement jsonElement) {
            //    // Extract the "title" property
            //    string title = jsonElement.GetProperty("title").GetString() ?? "Error";

            //    // Extract the "errors" property
            //    if (jsonElement.TryGetProperty("errors", out JsonElement errorsElement)) {
            //        var errors = errorsElement.EnumerateObject()
            //            .SelectMany(e => e.Value.EnumerateArray().Select(v => v.GetString()))
            //            .Where(v => !string.IsNullOrWhiteSpace(v)).ToList();

            //        // Format the error message
            //        var errorMessage = $"{title.Trim('.')}: {string.Join(", ", errors)}";
            //        ModelState.AddModelError(string.Empty, errorMessage);
            //    } else {
            //        ModelState.AddModelError(string.Empty, $"{title}: No detailed errors provided.");
            //    }
            //} else {
            //    ModelState.AddModelError(string.Empty, "An unknown error occurred.");
            //} 

            #endregion
        } catch (Exception ex) {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        // Clear the authentication cookie
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Clear JWT or any custom session values
        HttpContext.Session.Remove("JWTToken");
        HttpContext.Session.Remove("UserId");

        // Optionally: clear entire session
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }

    public async Task<bool> SinginUser(UserTokenDTO userToken, List<string> roles)
    {
        try {
            List<Claim> claims = [
                new ( ClaimTypes.Name, userToken?.Username! ),
                new ( "jwt", userToken?.Token! ),
                new ( ClaimTypes.NameIdentifier, userToken?.Username! )
            ];

            // Add roles to claims
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var schema = CookieAuthenticationDefaults.AuthenticationScheme;
            ClaimsIdentity claimsIdentity = new ( claims, schema );
            AuthenticationProperties authenticationProperties = new () { IsPersistent = true, AllowRefresh = true };
            await HttpContext.SignInAsync(schema, new ClaimsPrincipal(claimsIdentity), authenticationProperties);

            return true;
        } catch (Exception ex) {
            Console.WriteLine($"Error signing in user: {ex.Message}");

            return false;
        }
    }

    public IActionResult TestingCode()
    {
        return View();
    }
}