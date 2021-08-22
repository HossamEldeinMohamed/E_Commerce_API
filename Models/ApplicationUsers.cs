using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string TypeOfGoods { get; set; }
        public string Token { get; set; }

        public string  User_Name { get; set; }






        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Prouducts { get; set; }
    }
}
