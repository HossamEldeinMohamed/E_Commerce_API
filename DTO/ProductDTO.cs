using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string SupplierUserId { get; set; }
        public int CategoryId { get; set; }
        public int Quanity { get; set; }
        public double Discount { get; set; }
        public float Ranking { get; set; }
        public string Note { get; set; }
    }
}
