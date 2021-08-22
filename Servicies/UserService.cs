using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using E_Commerce_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce_Api.DTO.Helpers;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace E_Commerce_Api.Servicies
{
    public class UserService : IUser
    {
        //dependency injection
        #region
        private readonly UserManager<ApplicationUsers> UserManager;
        private readonly SignInManager<ApplicationUsers> SignInManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly IMapper _mapper;
        private readonly E_CommerceContext _Context;
        #endregion

        public UserService( SignInManager<ApplicationUsers> SignInManager, UserManager<ApplicationUsers> UserManager ,
            E_CommerceContext context , RoleManager<IdentityRole> RoleManager , IMapper mapper)
        {
            _Context = context;
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
            this.RoleManager = RoleManager;
            _mapper = mapper;
        }
        public async Task<Response> CreateUserAsync(RegisterDTO User)
        {
            //check if Email is exist Before
            #region
            var Found =await UserManager.FindByEmailAsync(User.Email);
            if (Found !=null )
                return new Response { Code = 400, Message = "the Email Is Already Exist" };
            #endregion
            //mapping DTO To model
            ApplicationUsers user = _mapper.Map<ApplicationUsers>(User);
            user.UserName = User.Email;
            //mapping getDTO
            GetUserDTO getUser = _mapper.Map<GetUserDTO>(user);
            //create user
            var result = await UserManager.CreateAsync(user, User.PasswordHash);
            //if result succeeded assign Role 
            if (result.Succeeded)
            {
                // Assign Role to User
                try
                {
                    var RoleAssign = await UserManager.AddToRolesAsync(user, User.UserRole);
                }
                catch (Exception e)
                {
                    await UserManager.DeleteAsync(user);
                    return new Response { Code = 200, Message = e.Message };
                }                   
                return new Response { Code = 200, Data = getUser, Message = "Create Successfully" };
            }
            //else return Error massage
            var errors = result.Errors.ToList();
            return new Response { Code = 401 , Message = errors[0].Description};
        }

        public async Task<Response> DeleteUserAsync(string id)
        {
            // find User with Id Included Products or Order
            var user = await _Context.Users.Include(x => x.Prouducts).Include(x=>x.Orders).Where(x => x.Id == id).FirstOrDefaultAsync();
            // delete user if not null 
            if (user != null)
            {
                // Delete User & retrun info
                try
                {
                    //mapping to getUserDTO
                    GetUserDTO getUser = _mapper.Map<GetUserDTO>(user);
                    await UserManager.DeleteAsync(user);

                    return new Response { Code = 200, Data = getUser, Message = "Delete Successfully" };
                }
                catch(Exception e)
                {
                    return new Response { Code = 400, Message = e.Message };
                }
            }
            //if user = nll
            return new Response { Code = 400, Message = "not exist in database" };
        }

        public async Task<Response> GetUserById(string id)
        {
            //find user with Id Included Products or Order
            var user = await _Context.Users.Include(x=> x.Prouducts).Where( x => x.Id ==  id).FirstOrDefaultAsync();
            // if user != null return user 
            if (user != null)
                return new Response { Code = 200, Data = user, Message = "Get Successfully" };
            //else return msg
            return new Response { Code = 400, Message = "Not Exist in Database" };
        }

        public async Task<IEnumerable<ApplicationUsers>> GetUsers()
        {
            //find user inculded products And select some property (manual Mapping)
            var user = await _Context.Users.Include(x => x.Prouducts).Select(x => new ApplicationUsers
            {
                Id = x.Id,
                Address = x.Address,
                Email = x.Email,
                CompanyName = x.CompanyName,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
                TypeOfGoods= x.TypeOfGoods,
                //ConcurrencyStamp = null,
                //SecurityStamp = null
                
            }).ToListAsync();
            if (user.Count!=0)
                  return user;
            return null;
        }

        public async Task<Response> UpdateUserAsync(string id, RegisterDTO User)
        {
            // find current user from database
            var CurrentUser = await UserManager.FindByIdAsync(id);
            var roles = await UserManager.GetRolesAsync(CurrentUser);
            if (CurrentUser != null)
            {
                CurrentUser.PhoneNumber = User.PhoneNumber;
                CurrentUser.Email = User.Email;
                CurrentUser.Address = User.Address;
                CurrentUser.CompanyName = User.CompanyName;
                //CurrentUser.PasswordHash = User.PasswordHash;
                CurrentUser.TypeOfGoods = User.TypeOfGoods;
                try
                {
                    await UserManager.RemoveFromRolesAsync(CurrentUser, roles);
                    await UserManager.AddToRolesAsync(CurrentUser, User.UserRole);
                    var result = await UserManager.UpdateAsync(CurrentUser);
                    return new Response { Code = 200, Data = CurrentUser, Message = "Updata Succesfuly" };
                }
                catch(Exception e)
                {
                    await UserManager.AddToRolesAsync(CurrentUser, roles);
                    return new Response { Code = 400, Message = e.Message };
                }
            }
            return new Response { Code = 400, Message = "not Exist in Database" };
        }
    }
}
