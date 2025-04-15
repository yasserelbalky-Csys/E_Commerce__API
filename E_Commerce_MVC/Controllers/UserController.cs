using System.Security.Claims;
using E_Commerce_MVC.ApiServices.AccountServices;
using E_Commerce_MVC.Models.UserViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace E_Commerce_MVC.Controllers;

public class UserController : Controller
{
	private readonly AccountService _accountService;
	private readonly HttpClient _httpClient;

	public UserController(HttpClient httpClient, AccountService accountService) {
		_httpClient = httpClient;
		_httpClient.BaseAddress = new Uri("http://localhost:5097/api/User/"); // API base URL
		_accountService = accountService;
	}

	public IActionResult Login() {
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel model) {
		if (!ModelState.IsValid) return View(model);

		try {
			// Send login request to the API
			var result = await _accountService.Login(model);
			Console.WriteLine($"result is: {result.ToJson()}");
			HttpContext.Session.SetString("JWTToken", result.Token);

			if (result != null) {
				var singinResult = await SinginUser(result);
			}

			return RedirectToAction("Index", "Home");
		} catch (Exception ex) {
			ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
		}

		return View(model);
	}

	public IActionResult Register() {
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterViewModel model) {
		if (!ModelState.IsValid) return View(model);

		try {
			// Send registration request to the API
			var result = await _accountService.Register(model);
			if (result.IsSuccessStatusCode)
				// Registration successful, redirect to login page
				return RedirectToAction("Login");

			ModelState.AddModelError(string.Empty, "Registration failed.");
		} catch (Exception ex) {
			ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
		}

		return View(model);
	}

	[HttpPost]
	public IActionResult Logout() {
		HttpContext.Session.Remove("JWTToken");
		return RedirectToAction("Index", "Home");
	}

	public async Task<bool> SinginUser(UserTokenDTO userToken) {
		try {
			List<Claim> claims = new List<Claim> {
				new(ClaimTypes.Name, userToken.Username),
				new("jwt", userToken.Token)
			};

			var schema = CookieAuthenticationDefaults.AuthenticationScheme;

			ClaimsIdentity claimsIdentity = new(claims, schema);
			AuthenticationProperties authenticationProperties = new() {
				IsPersistent = true,
				AllowRefresh = true
			};
			await HttpContext.SignInAsync(schema, new ClaimsPrincipal(claimsIdentity), authenticationProperties);
			return true;
		} catch (Exception ex) {
			Console.WriteLine($"Error signing in user: {ex.Message}");
			return false;
		}
	}
}