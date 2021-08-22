using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.Models;

namespace E_Commerce_Api.Servicies
{
    public class CategoryService : ICategory
    {
        private readonly E_CommerceContext _context;
        public CategoryService(E_CommerceContext context) => (_context) = (context);
        public async Task<Response> CreateCategoryAsync(CategoryDTO category)
        {
            var result = new Category
            {
                Name = category.Name,
                Description = category.Description
            };
            try
            {
                _context.Add(result);
               await _context.SaveChangesAsync();
                return new Response { Code = 200, Data = result, Message = "Create Successfully" };
            }
            catch(Exception e)
            {
                return new Response { Code = 400, Message = e.Message };
            }
        }

        public async Task<Response> DeleteCategoryAsync(int id)
        {
            var result =await  _context.Categories.Include(x => x.Prouducts).Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            if(result != null)
            {
                try
                {
                    _context.Remove(result);
                   await _context.SaveChangesAsync();
                    return new Response { Code = 200, Data = result, Message = "Delete Succcessfuly" };
                }
                catch(Exception e)
                {
                    return new Response { Code = 400, Message = e.ToString() };
                }
            }
            return new Response { Code = 401, Message = "Data Not Exist In Database" };
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var result =await _context.Categories.Include(x=>x.Prouducts).ToListAsync();
            if (result.Count != 0)
                return result;
            return null;
        }

        public async Task<Response> GetCategoryById(int id)
        {
            var result =await _context.Categories.Include(x=>x.Prouducts).Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            if (result != null)
                return new Response { Code = 200, Data = result, Message = "Get Successfully" };
            return new Response { Code = 401, Message = "Not Exist In Database" };
        }

        public async Task<Response> UpdateCategoryAsync(int id, CategoryDTO Category)
        {
            var CurrentCategory =await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            if (CurrentCategory == null)
                return new Response { Code = 401, Message = "Not Exist In Database" };
            CurrentCategory.Name = Category.Name;
            CurrentCategory.Description = Category.Description;
            try
            {
                _context.Update(CurrentCategory);
                await _context.SaveChangesAsync();
                return new Response { Code = 200, Data = CurrentCategory, Message = "Updata SuccessFully" };
            }
            catch(Exception e)
            {
                return new Response { Code = 400, Message = e.Message };
            }

        }
    }
}
