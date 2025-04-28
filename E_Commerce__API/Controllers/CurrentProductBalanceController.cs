using BLL.Contracts;
using BLL.DTOs.CurrentProductBalanceDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult Post(CurrentProductBalanceInsertDto init)
        {
            var res = _currentProductBalanceService.Insert(init);
            if (res == false)
            {
                return NotFound("Please Enter a valed Qty");
            }
            else
            {
                return Ok("Initial Balance Successed");
            }
        }
    }
}