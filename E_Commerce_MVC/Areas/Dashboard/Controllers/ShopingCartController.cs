using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ShopingCartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShopingCartController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShopingCartController
        public async Task<ActionResult> Index()
        {
            var carts = await _shoppingCartService.GetAllCarts();

            return View(carts);
        }

        // GET: ShopingCartController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var cart = await _shoppingCartService.GetByUserId(id.ToString());

            return View(cart);
        }

        // GET: ShopingCartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopingCartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShopingCartViewModel model)
        {
            try {
                if (!ModelState.IsValid) {
                    return View(model);
                }

                var cart = await _shoppingCartService.AddToCart(model);

                if (!cart) {
                    ModelState.AddModelError(string.Empty, "Failed to add item to cart.");

                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }
        }

        // GET: ShopingCartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopingCartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShopingCartViewModel model)
        {
            try {
                var cart = await _shoppingCartService.UpdateCart(model);

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }
        }

        // GET: ShopingCartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopingCartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try {
                await _shoppingCartService.DeleteCartProduct(id);

                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        public ActionResult ClearCart()
        {
            return View();
        }

        public async Task<ActionResult> ClearCartConfirmed()
        {
            try {
                await _shoppingCartService.ClearCart();

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View();
            }
        }
    }
}