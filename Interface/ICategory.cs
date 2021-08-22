using E_Commerce_API.DTO;
using E_Commerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Interface
{
    public interface ICategory
    {
        public Task<IEnumerable<Category>> GetCategories();
        public Task<Response> GetCategoryById(int id);
        public Task<Response> CreateCategoryAsync(CategoryDTO category);
        public Task<Response> UpdateCategoryAsync(int id, CategoryDTO Category);
        public Task<Response> DeleteCategoryAsync(int id);
    }
}
