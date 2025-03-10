using BLL.Contracts;
using BLL.DTOs.BrandDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        //Get Operation0
        [HttpGet]
        public IActionResult Get() {
            return Ok(_brandService.GetBrands());
        
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) {

            return Ok(_brandService.GetBrand(id));
        
        }

        [HttpPost]
        public IActionResult Post(BrandInsertDto brand)
        {

            _brandService.InsertBrand(brand);
            return Ok(brand);
        }

        [HttpPut]

        public IActionResult Put(BrandUpdateDto brand)
        {
            var temp=_brandService.UpdateBrand(brand);
            if (temp == 1)
            {
                return Ok();
            }
            else
            {
                return NotFound(new { message = "Brand not found." });
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _brandService.DeleteBrand(id);
            return Ok();
        }
    }
}
