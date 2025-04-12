using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers {
	public class CategoryController : Controller {
		public IActionResult Index() {
			List<WebApplication1.Models.Category> cats
				= new List<Models.Category>();

			for (int i = 0; i < 10; i++) {
				cats.Add(new Models.Category() {
					CategoryId = i,
					CategoryName = "Category " + i,
					CategoryDescription = "Description " + i,
					b_deleted = false
				});
			}

			return View(cats);
		}
	}
}
