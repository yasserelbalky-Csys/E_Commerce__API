using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class StoreController : Controller
    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: StoreController
        public async Task<ActionResult> Index()
        {
            var stores = await _storeService.GetAllStores();

            return View(stores);
        }

        // GET: StoreController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var store = await _storeService.GetStoreById(id);

            if (store == null) {
                return NotFound($"Store with ID {id} not found.");
            }

            return View(store);
        }

        // GET: StoreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StoreViewModel model)
        {
            try {
                var result = await _storeService.CreateStore(model);

                if (result == null) {
                    return NotFound($"Store with ID {model.StoreId} Cannot be Created");
                }

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, $"An error occurred in Creating the Store: {ex.Message}");

                return View();
            }
        }

        // GET: StoreController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var store = await _storeService.GetStoreById(id);

            if (store == null) {
                return NotFound($"Store with ID {id} not found.");
            }

            return View(store);
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StoreViewModel model)
        {
            try {
                if (ModelState.IsValid) {
                    var store = await _storeService.UpdateStore(model);

                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, $"An error occurred in Updating the Store: {ex.Message}");

                return View(model);
            }
        }

        // GET: StoreController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var store = await _storeService.GetStoreById(id);

            if (store == null) {
                return NotFound($"Store with ID {id} not found.");
            }

            return View(store);
        }

        // POST: StoreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try {
                await _storeService.DeleteStore(id);

                return RedirectToAction(nameof(Index));
            } catch {
                ModelState.AddModelError(string.Empty, $"An error occurred in Deleting the Store with ID {id}");

                return View(nameof(Index));
            }
        }
    }
}