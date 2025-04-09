using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using E_Commerce_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers {
	public class LoginController : Controller {
		private readonly HttpClient _httpClient;

		public LoginController(HttpClient httpClient) {
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:5097/api/User/"); // API base URL
		}

		[HttpGet]
		public IActionResult Index() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(LoginViewModel model) {
			if (!ModelState.IsValid) {
				return View(model);
			}

			try {
				// Send login request to the API
				var response = await _httpClient.PostAsJsonAsync("Login/Login", model);

				if (response.IsSuccessStatusCode) {
					// Retrieve the token from the API response
					var tokenResponse = await response.Content.ReadFromJsonAsync<UserTokenDto>();

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

		[HttpPost]
		public IActionResult Logout() {
			HttpContext.Session.Remove("JWTToken");
			return RedirectToAction("Index", "Login");
		}
	}

	public class UserTokenDto {
		public string Token { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
	}
}