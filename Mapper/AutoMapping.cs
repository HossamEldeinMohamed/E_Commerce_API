using AutoMapper;
using E_Commerce_Api.DTO.Get;
using E_Commerce_API.DTO;
using E_Commerce_API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RegisterDTO, ApplicationUsers>();
            CreateMap<ApplicationUsers, GetUserDTO>();
            CreateMap<RoleDTO, IdentityRole>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, GetProductDTO>().ForMember(dto => dto.GetCategoryDTO, opt => opt.MapFrom(x => x.Category)).ForMember(dto => dto.GetUserDTO, opt => opt.MapFrom(x => x.SupplierUser));
            CreateMap<CategoryDTO, Category>();
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderDetailDTO, OrderDetail>();
        }
    }
}
