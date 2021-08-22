using AutoMapper;
using E_Commerce_Api.DTO.Helpers;
using E_Commerce_Api.Interface;
using E_Commerce_API.DTO;
using E_Commerce_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Api.Servicies
{
    public class AuthenticateServer : IAuthenticate
    {
        private readonly AppSettings _appSettings;
        private readonly SignInManager<ApplicationUsers> SignInManager;
        private readonly UserManager<ApplicationUsers> UserManager;
        private readonly IMapper _mapper;
       public AuthenticateServer(IOptions<AppSettings> appSettings , SignInManager<ApplicationUsers> SignInManager, UserManager<ApplicationUsers> UserManager ,IMapper mapper)
        {
            _appSettings = appSettings.Value;
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
            _mapper = mapper;
        }

        public async Task<Response> Login(LoginDTO loginDTO)
        {
            try
            {
                var login = await SignInManager.PasswordSignInAsync(loginDTO.Email,
                         loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: true);
                if (!login.Succeeded)
                    return new Response { Code = 404, Message = "User Name Or Password Incorrect" };

                var user = await UserManager.FindByEmailAsync(loginDTO.Email);
                var Roles = await UserManager.GetRolesAsync(user);
                //authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                foreach (var role in Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),

                    //Subject = new ClaimsIdentity(new Claim[]
                    //{
                    //    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    //    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    //    // the JTI is used for our refresh token which we will be convering in the next video
                    //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),


                    //   new Claim(ClaimTypes.Role,String.Join(",",Roles))
                    //}),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                var getUser = _mapper.Map<GetUserDTO>(user);


                return new Response { Code = 404, Data = getUser, Message = "Sigin Successfully" };
            }
            catch (Exception e)
            {
                return new Response { Code = 404, Data = e.Data, Message = e.Message };
            }

        }

        public Task<Response> Logout()
        {
            throw new NotImplementedException();
        }
    }
}
