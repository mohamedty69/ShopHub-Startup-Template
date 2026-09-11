using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace myshop.BLL.DTOs.ReviewDTO
{
    public class CreateReviewDTO
    {
        [ValidateNever]
        public string userId {  get; set; }
        public int productId { get; set; }
        [MaxLength(500)]
        public string comment { get; set; }
        [RegularExpression(@"^[1-5]+$",ErrorMessage ="The rating must be from 1 to 5 only")]
        public int rating { get; set; }
    }
}
