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
	}
}