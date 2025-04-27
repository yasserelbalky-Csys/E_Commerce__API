using BLL.Contracts;
using BLL.DTOs.StoreDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_storeService.GetStores());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var store = _storeService.GetStore(id);
                if (store == null)
                {
                    return NotFound($"Store with ID {id} not found.");
                }
                return Ok(store);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }

        [HttpPost]
        public IActionResult Post(StoreInsertDto store)
        {
            _storeService.InsertStore(store);
            return Ok("Store Added Successfully");
        }

        [HttpPut]
        public IActionResult PUT(StoreUpdateDto store)
        {
            _storeService.UpdateStore(store);
            return Ok("Store Updated Successfully");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _storeService.DeleteStore(id);
            return Ok("Store Deleted Successfully");
        }

    }
}
