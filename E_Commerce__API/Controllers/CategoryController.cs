using BLL.Contracts;
using BLL.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetCategories());
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            return Ok(_categoryService.GetCategory(id));
        }


        [HttpPost]
        public IActionResult Post(CategoryInsertDto cat) {
            _categoryService.InsertCategory(cat);
            return Ok();
        }


        [HttpPut]

        public IActionResult Put(CategoryUpdateDto cat) {

            _categoryService.UpdateCategory(cat);
            return Ok();
        
        }


        [HttpDelete]

        public IActionResult Delete(int id) {
            _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
