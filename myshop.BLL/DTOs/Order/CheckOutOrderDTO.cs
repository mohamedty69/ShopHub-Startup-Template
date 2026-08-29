using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace myshop.BLL.DTOs.Order
{
    public class CheckOutOrderDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength (100)]
        public string Address { get; set; }
        public string City { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
