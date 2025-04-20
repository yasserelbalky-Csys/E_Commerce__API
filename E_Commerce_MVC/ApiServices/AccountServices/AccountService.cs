using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using E_Commerce_MVC.Models.UserViewModel;
using NuGet.Protocol;

namespace E_Commerce_MVC.ApiServices.AccountServices
{
	public class AccountService
	{
		private readonly HttpClient _httpClient;

		public AccountService(HttpClient httpClient) {
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:5097/api/User/");
		}

		public async Task<HttpResponseMessage> Register(RegisterViewModel model) {
			var response = await _httpClient.PostAsJsonAsync("Post/Register", model);
			return response;
		}

		public async Task<UserTokenDTO?> Login(LoginViewModel model) {
			var response = await _httpClient.PostAsJsonAsync("Login/Login", model);
			if (response.IsSuccessStatusCode) {
				var userToken = await response.Content.ReadFromJsonAsync<UserTokenDTO>();

				return userToken;
			} else {
				return null;
			}
		}

		public List<Claim> DecodeToken(string token) {
			if (string.IsNullOrEmpty(token)) {
				return new List<Claim>();
			}

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			return [.. jwtToken.Claims];
		}

		public List<string> GetRolesFromToken(string token) {
			var claims = DecodeToken(token);
			foreach (var claim in claims) {
				Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
			}

			return claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
		}
	}
}