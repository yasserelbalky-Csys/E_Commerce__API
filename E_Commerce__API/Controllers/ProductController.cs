using System.Security.Claims;
using BLL.Contracts;
using BLL.DTOs.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce__API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		protected readonly IProductService _productService;

		public ProductController(IProductService productService) {
			_productService = productService;
		}

		[HttpGet]
		public IActionResult Get() {

			return Ok(_productService.GetProducts());
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id) {

			return Ok(_productService.GetProduct(id));
		}
		[HttpPost]

		[Authorize(Roles = "Admin")]
		public IActionResult Post(ProductInsertDto product) {

			if (!User.IsInRole("Admin")) {
				return Forbid(); // Returns 403 Forbidden
			}

			_productService.InsertProduct(product);
			return Ok();
			//_productService.InsertProduct(product);
			//return Ok();
		}
		[Authorize(Roles = "Admin")]
		[HttpPut]
		public IActionResult Put(ProductUpdateDto product) {
			_productService.UpdateProduct(product);
			return Ok();
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete]
		public IActionResult DeleteById(int id) {
			_productService.DeleteProduct(id);
			return Ok();
		}
	}
}
