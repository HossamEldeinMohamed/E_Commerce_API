using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SignController : ControllerBase
    {
       // private readonly SignInManager<IdentityUser> SignInManager;
        private readonly IAuthenticate _authenticateService;
        public SignController( IAuthenticate authenticateService)
        {
            //this.SignInManager = SignInManager;
            _authenticateService = authenticateService;
        }
        [HttpPost("LoginAsync")]
        public async Task<Response> LogIn(LoginDTO userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticateService.Login(userModel);
                return result;
            }
            return new Response { Data = ModelState.Values.SelectMany(v => v.Errors), Message = "Not Implemented" };
        }
    }
}
