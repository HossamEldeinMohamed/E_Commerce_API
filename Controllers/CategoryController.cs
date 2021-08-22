using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.AspNetCore.Authorization;
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
   // [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _CategoryService;
        public CategoryController(ICategory categoryService) => (_CategoryService) = (categoryService);

        [HttpPost("CreateCategoryAsync")]
        [Produces("application/json")]
        public async Task<Response> CreateCategoryAsync(CategoryDTO Category)
        {
            if (ModelState.IsValid)
            {
                var result = await _CategoryService.CreateCategoryAsync(Category);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpGet("GetCategoryAsync")]
        [Produces("application/json")]
        
        public async Task<IActionResult> GetCategoryAsync()
        {
            // Category result = new Category();
            if (ModelState.IsValid)
            {
                var result = await _CategoryService.GetCategories();
                if (result != null)
                {
                    return StatusCode(200, new Response { Data = result, Message = "Get Successfully" });
                }
                return StatusCode(400, new Response { Data = result, Message = "No data Exist" }); 
            }
            return StatusCode(501, new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" });
        }
        [HttpGet("GetCategoryByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> GetCategoryByIdAsync(int Id)
        {
            // Category result = new Category();
            if (ModelState.IsValid)
            {
                var result = await _CategoryService.GetCategoryById(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpPut("UpdataByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> UpdataByIdAsync(int Id, CategoryDTO Category)
        {
            // Category result = new Category();
            if (ModelState.IsValid)
            {
                var result = await _CategoryService.UpdateCategoryAsync(Id, Category);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpDelete("DeleteByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> DeleteCategoryAsync(int Id)
        {
            // Category result = new Category();
            if (ModelState.IsValid)
            {
                var result = await _CategoryService.DeleteCategoryAsync(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }

    }

}

