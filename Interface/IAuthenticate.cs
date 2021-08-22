using E_Commerce_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Interface
{
   public interface IAuthenticate
    {
        public Task<Response> Login(LoginDTO loginDTO);
        public Task<Response> Logout();
    }
}
