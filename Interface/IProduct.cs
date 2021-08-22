using E_Commerce_API.Models;
using E_Commerce_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Interface
{
    public interface IProduct
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Response> GetProductById(int id);
        public Task<Response> CreateProductAsync(ProductDTO Product);
        public Task<Response> UpdateProductAsync(int id, ProductDTO Product);
        public Task<Response> DeleteProductAsync(int id);
    }
}
