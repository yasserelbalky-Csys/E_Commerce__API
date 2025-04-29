using BLL.Contracts;
using BLL.DTOs.CurrentProductBalanceDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrentProductBalanceController : ControllerBase
    {
        private readonly ICurrentProductBalanceService _currentProductBalanceService;

        public CurrentProductBalanceController(ICurrentProductBalanceService currentProductBalanceService)
        {
            _currentProductBalanceService = currentProductBalanceService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post(CurrentProductBalanceInsertDto init)
        {
            var res = _currentProductBalanceService.Insert(init);
            if (!res)
                return BadRequest("Please enter a valid quantity greater than 0.");

            return Ok("Initial balance added successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update(CurrentProductBalanceUpdateDto dto)
        {
            var result = _currentProductBalanceService.update(dto);
            if (!result)
                return NotFound("Product balance not found for update.");

            return Ok("Product balance updated successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _currentProductBalanceService.DeleteById(id);
            if (!result)
                return NotFound("Product balance not found for deletion.");

            return Ok("Product balance deleted successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<CurrentProductBalanceListDto>> GetAll()
        {
            var result = _currentProductBalanceService.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public ActionResult<CurrentProductBalanceListDto> GetById(int id)
        {
            var result = _currentProductBalanceService.GetById(id);
            if (result == null)
                return NotFound("Product balance not found.");

            return Ok(result);
        }
    }
}