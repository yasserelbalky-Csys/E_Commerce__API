using E_Commerce_MVC.Models.UserViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers
{
	public class UserController : Controller
	{
		private readonly HttpClient _httpClient;

		public UserController(HttpClient httpClient) {
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:5097/api/User/"); // API base URL
		}

		public IActionResult Login() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model) {
			if (!ModelState.IsValid) {
				return View(model);
			}

			try {
				// Send login request to the API
				var response = await _httpClient.PostAsJsonAsync("Login/Login", model);

				if (response.IsSuccessStatusCode) {
					// Retrieve the token from the API response
					var tokenResponse = await response.Content.ReadFromJsonAsync<UserTokenDTO>();

					// Store the token in session
					HttpContext.Session.SetString("JWTToken", tokenResponse.Token);

					// Redirect to the home page or any other page
					return RedirectToAction("Index", "Home");
				} else {
					ModelState.AddModelError(string.Empty, "Invalid username or password.");
				}
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
			if (!ModelState.IsValid) {
				return View(model);
			}

			try {
				// Send registration request to the API
				var response = await _httpClient.PostAsJsonAsync("Post/Register", model);
				if (response.IsSuccessStatusCode) {
					// Registration successful, redirect to login page
					return RedirectToAction("Login");
				} else {
					ModelState.AddModelError(string.Empty, "Registration failed.");
				}
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
	}
}