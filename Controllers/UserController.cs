using E_Commerce_API.Models;
using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _UserService;

        public UserController(IUser UserService) => (_UserService) = (UserService);

        [HttpPost("CreateUserAsync")]
        [Produces("application/json")]
        public async Task<Response> CreateUserAsync(RegisterDTO User)
        {
            // User result = new User();
            if (ModelState.IsValid)
            {
                var result = await _UserService.CreateUserAsync(User);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpGet("GetUserAsync")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserAsync()
        {
            // User result = new User();
            if (ModelState.IsValid)
            {
                var result = await _UserService.GetUsers();
                if (result != null)
                {
                    return StatusCode(200, new Response { Data = result, Message = "Get Successfully" });
                }
                return StatusCode(400, new Response { Data = result, Message = "No data Exist" });
            }
            return StatusCode(501, new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" });
        }
        [HttpGet("GetUserByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> GetUserByIdAsync(string Id)
        {
            // User result = new User();
            if (ModelState.IsValid)
            {
                var result = await _UserService.GetUserById(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpPut("UpdataByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> UpdataByIdAsync(string Id, RegisterDTO User)
        {
            // User result = new User();
            if (ModelState.IsValid)
            {
                var result = await _UserService.UpdateUserAsync(Id, User);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpDelete("DeleteByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> DeleteUserAsync(string Id)
        {
            // User result = new User();
            if (ModelState.IsValid)
            {
                var result = await _UserService.DeleteUserAsync(Id);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }

    }
}
