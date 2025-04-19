using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace E_Commerce_MVC.Areas.Admin.Controllers
{
	[Area("Dashboard")]
	[Authorize(Roles = "Admin")]
	public class HomeController : Controller
	{
		// GET: HomeController
		public ActionResult Index()
		{
			if (User.IsInRole("Admin"))
				Console.WriteLine("Admin");
			else
				Console.WriteLine("Not Admin");

			return View();
		}
	}
}