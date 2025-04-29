using Azure.Core;
using BLL.Contracts;
using BLL.DTOs.OrderDetailsDtos;
using BLL.DTOs.OrderDtos;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderservice;
        private readonly IHelperService _helperService;

        public OrderController(IOrderService orderservice, IHelperService helperservice)
        {
            _orderservice = orderservice;
            _helperService = helperservice;
        }

      
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderservice.GetOrders());
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostMaster(OrderInsertDto order)
        {
            //get userId
            var claimsIdentity = User.Identity as ClaimsIdentity;

            // Ensure the identity is not null
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // Ensure the claim exists
            if (userIdClaim == null)
            {
                return BadRequest(new { message = "User ID claim is missing from the token." });
            }

            var userId = userIdClaim.Value;

            order.UserId = userId; // Ensure UserId is assigned before inserting

            var res = _orderservice.InsertOrder(order);
            if (res == false)
            {
                return NotFound("No Available Qty");
            }
            else
            {
                return Ok("Order Placed Successfuly");
            }
        }

        [HttpGet("{orderid:int}")]
        public IActionResult GetById(int orderid)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            // Ensure the identity is not null
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // Ensure the claim exists
            if (userIdClaim == null)
            {
                return BadRequest(new { message = "User ID claim is missing from the token." });
            }

            var userId = userIdClaim.Value;

            var found = _orderservice.GetOrderById(orderid);
            if (found != null)
            {
                return Ok(found);
            }
            else
            {
                return NotFound("Order Not Found");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetDetailsByorderid(int masterid)
        {
            var result = _orderservice.GetOrderDetailsById(masterid);
            if (result == null)
            {
                return NotFound("No order details found.");
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(OrderUpdateRequestDto request)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            // Ensure the identity is not null
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            // Ensure the claim exists
            if (userIdClaim == null)
            {
                return BadRequest(new { message = "User ID claim is missing from the token." });
            }

            var userId = userIdClaim.Value;
            var found = _orderservice.UpdateOrder(request.Order, request.Details);
            //  var found = _orderservice.UpdateOrder(order, details);
            if (found == 1)
            {
                return Ok(found);
            }
            else
            {
                return NotFound("Order Not Found");
            }
        }

        [HttpPut("{OrderNo}")]
        public IActionResult Update(int OrderNo)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            // Ensure the identity is not null
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            // Ensure the claim exists
            if (userIdClaim == null)
            {
                return BadRequest(new { message = "User ID claim is missing from the token." });
            }

            var userId = userIdClaim.Value;
            var found = _helperService.UpdateOrderStatus(OrderNo);
            //  var found = _orderservice.UpdateOrder(order, details);
            if (found == 1)
            {
                return Ok("Order Confirmed");
            }
            else
            {
                return NotFound("Order Not Found");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int OrderNo)
        {
            var res = _orderservice.DeleteOrder(OrderNo);
            if (res == 0)
            {
                return Ok("Order Deleted Successfully");
            }
            else
            {
                return NotFound("Order Not Found");
            }
        }
    }
}