using E_Commerce_Api.Interface;
using E_Commerce_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _OrderService;
        public OrderController(IOrder order) => (_OrderService) = (order);

        [HttpPost("CreateOrderAsync")]
        [Produces("application/json")]
        public async Task<Response> CreateOrderAsync(OrderDTO Order)
        {
            // Order result = new Order();
            if (ModelState.IsValid)
            {
                var result = await _OrderService.CreateOrderAsync(Order);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpGet("GetOrderAsync")]
        [Produces("application/json")]
        public async Task<IActionResult> GetOrderAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _OrderService.GetOrders();
                if (result != null)
                {
                    return StatusCode(200, new Response { Data = result, Message = "Get Successfully" });
                }
                return StatusCode(400, new Response { Data = result, Message = "No data Exist" });
            }
            return StatusCode(501, new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" });
        }
        [HttpGet("GetOrderByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> GetOrderByIdAsync(int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _OrderService.GetOrderById(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpDelete("DeleteByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> DeleteOrderAsync(int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _OrderService.CancelOrderAsync(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
    }

}
