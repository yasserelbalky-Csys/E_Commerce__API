using BLL.Contracts;
using BLL.DTOs.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get() {

            return Ok(_productService.GetProducts());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            return Ok(_productService.GetProduct(id));
        }
        [HttpPost]
        public IActionResult Post(ProductInsertDto product)
        {

            _productService.InsertProduct(product);
            return Ok();
        }
        [HttpPut]

        public IActionResult Put(ProductUpdateDto product)
        {
            _productService.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteById(int id) {
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
