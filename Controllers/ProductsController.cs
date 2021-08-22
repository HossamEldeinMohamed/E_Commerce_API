using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Api.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _ProductService;
    
       

        public ProductsController(IProduct ProductService) => (_ProductService) = (ProductService);
        [HttpPost("CreateProductAsync")]
        [Produces("application/json")]
        public async Task<Response> CreateProductAsync(ProductDTO Product)
        {
            // Product result = new Product();
            if (ModelState.IsValid)
            {
                var result = await _ProductService.CreateProductAsync(Product);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpGet("GetProductAsync")]
        [Produces("application/json")]
        public async Task<IActionResult> GetProductAsync()
        {
            // Product result = new Product();
            if (ModelState.IsValid)
            {
                var result = await _ProductService.GetProducts();
                if (result != null)
                {
                    return StatusCode(200, new Response { Data = result, Message = "Get Successfully" });
                }
                return StatusCode(400, new Response { Data = result, Message = "No data Exist" });

            }
            return StatusCode(501, new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" });
        }
        [HttpGet("GetProductByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> GetProductByIdAsync(int Id)
        {
            // Product result = new Product();
            if (ModelState.IsValid)
            {
                var result = await _ProductService.GetProductById(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpPut("UpdataByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> UpdataByIdAsync(int Id, ProductDTO Product)
        {
            // Product result = new Product();
            if (ModelState.IsValid)
            {
                var result = await _ProductService.UpdateProductAsync(Id, Product);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpDelete("DeleteByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> DeleteProductAsync(int Id)
        {
            // Product result = new Product();
            if (ModelState.IsValid)
            {
                var result = await _ProductService.DeleteProductAsync(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }

    }
}

