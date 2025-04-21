using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BLL.Services
{
	internal class SessionManager : ISessionManager
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SessionManager(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public void Set<T>(string key, T value)
		{
			var session = _httpContextAccessor.HttpContext?.Session;
			if (session != null) {
				session.SetString(key, JsonConvert.SerializeObject(value));
			}
		}

		public T? Get<T>(string key)
		{
			var session = _httpContextAccessor.HttpContext?.Session;
			if (session != null && session.GetString(key) is string value) {
				return JsonConvert.DeserializeObject<T>(value);
			}

			return default;
		}

		public void Remove(string key)
		{
			_httpContextAccessor.HttpContext?.Session.Remove(key);
		}
	}
}