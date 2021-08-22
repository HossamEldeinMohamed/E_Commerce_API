using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.DTO;
using E_Commerce_API.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace E_Commerce_Api.Servicies
{
    public class RoleService : IRole
    {
       //dependency injection
        #region
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper _mapper;
        #endregion
        public RoleService (RoleManager<IdentityRole> roleManager, E_CommerceContext context , IMapper mapper)
        {
            this.roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<Response> CreateroleAsync(RoleDTO Role)
        {
            // use mapping RoleDto to IdentityRole
            var CreaeRole = _mapper.Map<IdentityRole>(Role);

            // use CreateRoleAsync func & return response 
            try
            {
                var result = await roleManager.CreateAsync(CreaeRole);
                return new Response { Code = 200, Data = CreaeRole, Message = "Create Successfuly" };
            }
           catch(Exception e)
            {
                return new Response { Code = 401, Message = e.Message};
            }
        }

        public async Task<Response> DeleteRoleAsync(string Name)
        {
            var role = await roleManager.FindByNameAsync(Name);

            if (role != null)
            {
                // use Delete func & return Response
                try
                {
                    var result = await roleManager.DeleteAsync(role);
                    return new Response { Code = 200, Data = role, Message = "Delete Successfully" };
                }
               catch(Exception e)
                {
                    return new Response { Code = 405, Data = role, Message = e.Message };
                }  
            }
            return new Response { Code = 404, Data = role, Message = "This role Not found In data base" };
        }

        public async Task<Response> GetRoleById(string Name)
        {
            var role = await roleManager.FindByNameAsync(Name);

            if (role != null)
                    return new Response { Code = 200, Data = role, Message = "Get Successfully" };
            return new Response { Code = 404, Data = role, Message = "This role Not found In data base" };
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            var roles =await roleManager.Roles.ToListAsync();

            if (roles.Count != 0)
                return roles;
            return null;
        }

        public async Task<Response> UpdateRoleAsync(string Name, RoleDTO role)
        {
            //Find CurrntRole
            var CurrentRole = await roleManager.FindByNameAsync(Name);
            //check!null
            if (CurrentRole == null)
                return new Response { Code = 401, Message = "This role Not found In data base" };
            //assign
            CurrentRole.Name = role.Name;
            //use update func & return Response
            try
            {
                var result = await roleManager.UpdateAsync(CurrentRole);
                return new Response { Code = 200, Data = CurrentRole, Message = "Updata SuccessFully" };
            }
            catch(Exception e)
            {
                return new Response { Code = 405, Message = e.Message };
            }
        }
    }
}
