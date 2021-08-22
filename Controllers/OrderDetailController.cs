using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
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
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetail _OrderDetailService;

        public OrderDetailController(IOrderDetail OrderDetailService) => (_OrderDetailService) = (OrderDetailService);

        [HttpPost("CreateOrderDetailAsync")]
        [Produces("application/json")]
        public async Task<Response> CreateOrderDetailAsync(IList<OrderDetailDTO> OrderDetail , int orderId)
        {
            // OrderDetail result = new OrderDetail();
            if (ModelState.IsValid)
            {
                var result = await _OrderDetailService.CreateOrderDetailAsync(OrderDetail , orderId);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpGet("GetOrderDetailAsync")]
        [Produces("application/json")]
        public async Task<IActionResult> GetOrderDetailAsync()
        {
            // OrderDetail result = new OrderDetail();
            if (ModelState.IsValid)
            {
                var result = await _OrderDetailService.GetOrderDetails();
                if (result != null)
                {
                    return StatusCode(200, new Response { Data = result, Message = "Get Successfully" });
                }
                return StatusCode(400, new Response { Data = result, Message = "No data Exist" });
            }
            return StatusCode(501, new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" });
        }
        [HttpGet("GetOrderDetailByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> GetOrderDetailByIdAsync(int Id)
        {
            // OrderDetail result = new OrderDetail();
            if (ModelState.IsValid)
            {
                var result = await _OrderDetailService.GetOrderDetailById(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        //[HttpPut("UpdataByIdAsync")]
        //[Produces("application/json")]
        //public async Task<Response> UpdataByIdAsync(int Id, OrderDetailDTO OrderDetail)
        //{
        //    // OrderDetail result = new OrderDetail();
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _OrderDetailService.UpdateOrderDetailAsync(Id, OrderDetail);
        //        return result;
        //    }
        //    return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        //}
        //[HttpDelete("DeleteByOrderIdAsync")]
        //[Produces("application/json")]
        //public async Task<Response> DeleteOrderDetailAsync(int Id)
        //{
        //    // OrderDetail result = new OrderDetail();
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _OrderDetailService.DeleteOrderDetailAsync(Id);
        //        return result;
        //    }
        //    return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        //}
        [HttpDelete("DeleteByProductIdAsync")]
        [Produces("application/json")]
        public async Task<Response> DeleteItemFromOrderDetailAsync(int OrderId , int ProductId)
        {
            // OrderDetail result = new OrderDetail();
            if (ModelState.IsValid)
            {
                var result = await _OrderDetailService.DeleteItemFromOrderDetailAsync(OrderId , ProductId);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        


    }
}

