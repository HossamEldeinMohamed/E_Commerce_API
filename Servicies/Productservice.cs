using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.Models;
using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using E_Commerce_Api.DTO.Get;

namespace E_Commerce_Api.Servicies
{
    public class Productservice : IProduct
    {
        //dependency injection
        #region
        private readonly E_CommerceContext _context;
        private readonly IMapper _mapper;
        #endregion
        public Productservice(E_CommerceContext context, IMapper mapper) 
        {
            _context = context; 
            _mapper = mapper;
        } 
        public async Task<Response> CreateProductAsync(ProductDTO ProductDto)
        {
            //use AutoMapper ProductDto:Product
            var product = _mapper.Map<Product>(ProductDto);

            //add product to db & return response
            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return new Response { Code = 200, Data = product, Message = "Create Succesfully" };
            }
            catch(Exception e)
            {
                return new Response { Code = 400, Message = e.Message };
            }
        }

        public async Task<Response> DeleteProductAsync(int id)
        {
            //find product from DB
            var product = await _context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();

            //chech ! null => remove From DB & return Response 
            if (product != null)
            {
                try
                {
                    _context.Remove(product);
                    await _context.SaveChangesAsync();
                    return new Response { Code = 200, Data = product, Message = "Delete Successfully" };
                }
                catch(Exception e)
                {
                    return new Response { Code = 400, Message = e.ToString() };
                }
            }

            //product = null
            return new Response { Code = 401, Data = null, Message = "Not Exist In DataBase" };
        }

        public async Task<Response> GetProductById(int id)
        {
          var product = await _context.Products.Where(x => x.ProductId == id).Include(x => x.SupplierUser).Include(x => x.Category).FirstOrDefaultAsync();

          var result =  _mapper.Map<GetProductDTO>(product);
            if (product != null)
                return new Response { Code = 200, Data = result, Message = "Get Successfully" };
            return new Response { Code = 401, Data = null, Message = "Not Exist In DataBase" };
        }

        public async Task<IEnumerable<Product>>GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            if (products.Count != 0)
                return products;
            return null;
        }

        public async Task<Response> UpdateProductAsync(int id, ProductDTO ProductDto)
        {
            var curruntProduct =await _context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();

            //if urruntProduct ! null update in DB and return response
            if(curruntProduct != null)
            {
                curruntProduct.Name = ProductDto.Name;
                curruntProduct.Description = ProductDto.Description;
                curruntProduct.CategoryId = ProductDto.CategoryId;
                curruntProduct.SupplierUserId = ProductDto.SupplierUserId;
                curruntProduct.Quanity = ProductDto.Quanity;
                curruntProduct.Ranking = ProductDto.Ranking;
                curruntProduct.Discount = ProductDto.Discount;
                curruntProduct.Note = ProductDto.Note;
                curruntProduct.Price = ProductDto.Price;
                try
                {
                    _context.Products.Update(curruntProduct);
                    await _context.SaveChangesAsync();
                    return new Response { Code = 200, Data = curruntProduct, Message = "Update Succesfully" };
                }
                catch(Exception e)
                {
                    return new Response { Code = 400, Message = e.Message };
                }
            }
            return new Response { Code = 401, Data = null, Message = "Not Exist In DataBase" };

        }
    }
}
