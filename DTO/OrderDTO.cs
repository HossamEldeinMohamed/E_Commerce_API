using E_Commerce_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.DTO
{
    public class OrderDTO
    {
        public string CustomerId { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ShipDate { get; set; }
        public virtual IList<OrderDetailDTO> OrderDetailes { get; set; }

    }
}
