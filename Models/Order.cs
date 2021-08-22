using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string CustomerUserId { get; set; }
        public int OrderNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ShipDate { get; set; }
        public double Total { get; set; }


        public virtual ApplicationUsers CustomerUser { get; set; }
        public virtual ICollection<OrderDetail> OrderDetailes { get; set; }
    }
}
