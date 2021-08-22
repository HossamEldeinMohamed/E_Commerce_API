//using E_Commerce_API.DTO;
//using E_Commerce_Api.Interface;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace E_Commerce_Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomerController : ControllerBase
//    {
//        private readonly ICustomer _CustomerService;

//        public CustomerController(ICustomer CustomerService) => (_CustomerService) = (CustomerService);

//        [HttpPost("CreateCustomerAsync")]
//        [Produces("application/json")]
//        public async Task<Response> CreateCustomerAsync(RegisterDTO Customer)
//        {
//            // Customer result = new Customer();
//            if (ModelState.IsValid)
//            {
//                var result = await _CustomerService.CreateCustomerAsync(Customer);
//                return result;
//            }
//            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
//        }
//        [HttpGet("GetCustomerAsync")]
//        [Produces("application/json")]
//        public async Task<IActionResult> GetCustomerAsync()
//        {
//            // Customer result = new Customer();
//            if (ModelState.IsValid)
//            {
//                var result = await _CustomerService.GetCustomers();
//                if (result != null)
//                {
//                    return StatusCode(200, new Response { Data = result, Message = "Get Successfully" });
//                }
//                return StatusCode(400, new Response { Data = result, Message = "No data Exist" });
//            }
//            return StatusCode(501, new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" });
//        }
//        [HttpGet("GetCustomerByIdAsync")]
//        [Produces("application/json")]
//        public async Task<Response> GetCustomerByIdAsync(string Id)
//        {
//            // Customer result = new Customer();
//            if (ModelState.IsValid)
//            {
//                var result = await _CustomerService.GetCustomerById(Id);
//                return result;
//            }
//            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
//        }
//        [HttpPut("UpdataByIdAsync")]
//        [Produces("application/json")]
//        public async Task<Response> UpdataByIdAsync(string Id, RegisterDTO Customer)
//        {
//            // Customer result = new Customer();
//            if (ModelState.IsValid)
//            {
//                var result = await _CustomerService.UpdateCustomerAsync(Id, Customer);
//                return result;
//            }
//            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
//        }
//        [HttpDelete("DeleteByIdAsync")]
//        [Produces("application/json")]
//        public async Task<Response> DeleteCustomerAsync(string Id)
//        {
//            // Customer result = new Customer();
//            if (ModelState.IsValid)
//            {
//                var result = await _CustomerService.DeleteCustomerAsync(Id);
//                return result;
//            }
//            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
//        }

//    }
//}

