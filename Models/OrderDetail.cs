using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Models
{
    public class OrderDetail
    {
        [Key]        
        public int OrderId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public double price { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }


    }
}
