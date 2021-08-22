using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SupplierUserId { get; set; }
        public int CategoryId { get; set; }
        public int Quanity { get; set; }
        public double Discount { get; set; }
        public float Ranking { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }

        public  ApplicationUsers SupplierUser { get; set; }
        public  Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetailes { get; set; }

    }
}
