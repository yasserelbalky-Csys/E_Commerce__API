using BLL.Contracts;
using BLL.DTOs.SubCategoryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce__API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SubCategoryController : ControllerBase
	{
		protected readonly ISubCategoryService _subCategoryService;

		public SubCategoryController(ISubCategoryService subCategoryService)
		{
			_subCategoryService = subCategoryService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_subCategoryService.GetSubCategories());
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(_subCategoryService.GetSubCategory(id));
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Post(SubCategoryInsertDto subcat)
		{
			_subCategoryService.InsertSubCategory(subcat);
			return Ok();
		}

		[Authorize(Roles = "Admin")]
		[HttpPut]
		public IActionResult Put(SubCategoryUpdateDto sub)
		{
			_subCategoryService.UpdateSubCategory(sub);
			return Ok();
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete]
		public IActionResult Delete(int id)
		{
			_subCategoryService.DeleteCategory(id);
			return Ok();
		}
	}
}