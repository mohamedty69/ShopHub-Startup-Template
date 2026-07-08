using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace myshop.BLL.DTOs.User
{
    public class RegisterDTO
    {
        [MaxLength(20)]
        public string FirstName { get; set; }  = string.Empty;
        [MaxLength(20)]
        public string LastName { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(pattern: @"^[0-9]+$", ErrorMessage = "Invalid phone number Phone number need to contains numbers.")]
        public string PhoneNumber { get; set; } = String.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; } = String.Empty;
    }
}
