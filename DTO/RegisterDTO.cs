using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce_API.DTO
{
    
    public class RegisterDTO
    {
        
        [Required]
        [Display(Name = "User Name")]
        public string User_Name { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "the Password must be at least {2} and at max {1} characters long ", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("PasswordHash", ErrorMessage = "The Password and confirmation Password don't match ")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Type Of Goods")]
        public string TypeOfGoods { get; set; }

        public IList<string> UserRole { get; set; }
    }
}
