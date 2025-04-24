using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult CartIndex()
        {
            return View();
        }
    }
}