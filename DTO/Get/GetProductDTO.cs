using E_Commerce_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.DTO.Get
{
    public class GetProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quanity { get; set; }
        public string SupplierUserId { get; set; }
        public int CategoryId { get; set; }
        public double Discount { get; set; }
        public float Ranking { get; set; }
        public string Note { get; set; }

        public CategoryDTO GetCategoryDTO { get; set; }
        public GetUserDTO GetUserDTO { get; set; }

    }
}

