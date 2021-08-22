using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.DTO;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _roleService;

        public RoleController(IRole roleService) => (_roleService) = (roleService);

        [HttpPost("CreateSupplierAsync")]
        [Produces("application/json")]
        public async Task<Response> CreateRoleAsync(RoleDTO Role)
        {
            // Supplier result = new Supplier();
            if (ModelState.IsValid)
            {
                var result = await _roleService.CreateroleAsync(Role);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpGet("GetRolesAsync")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRolesAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.GetRoles();
                if (result != null)
                {
                    return StatusCode(200, new Response { Data = result, Message = "Get Successfully" });
                }
                return StatusCode(400, new Response { Data = result, Message = "No data Exist" });

            }
            return StatusCode(501, new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" });
        }
        [HttpGet("GetRoleByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> GetRoleByNameAsync(string Name)
        {
            // Role result = new Role();
            if (ModelState.IsValid)
            {
                var result = await _roleService.GetRoleById(Name);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpPut("UpdataByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> UpdataByIdAsync(string Name, RoleDTO Role)
        {
            // Role result = new Role();
            if (ModelState.IsValid)
            {
                var result = await _roleService.UpdateRoleAsync(Name, Role);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
        [HttpDelete("DeleteRoleByIdAsync")]
        [Produces("application/json")]
        public async Task<Response> DeleteRoleAsync(string Name)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.DeleteRoleAsync(Name);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }

    }
}

