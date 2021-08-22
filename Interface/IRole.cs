using E_Commerce_Api.DTO;
using E_Commerce_API.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Interface
{
   public interface IRole
    {
        public Task<IEnumerable<IdentityRole>> GetRoles();
        public Task<Response> GetRoleById(string id);
        public Task<Response> CreateroleAsync(RoleDTO Role);
        public Task<Response> UpdateRoleAsync(string id, RoleDTO Category);
        public Task<Response> DeleteRoleAsync(string id);
    }
}
