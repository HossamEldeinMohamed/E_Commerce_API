using E_Commerce_API.DTO;
using E_Commerce_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Interface
{
    public interface IUser
    {
        public Task<IEnumerable<ApplicationUsers>> GetUsers();
        public Task<Response> GetUserById(string id);
        public Task<Response> CreateUserAsync(RegisterDTO User);
        public Task<Response> UpdateUserAsync(string id, RegisterDTO User);
        public Task<Response> DeleteUserAsync(string id);
    }
}
