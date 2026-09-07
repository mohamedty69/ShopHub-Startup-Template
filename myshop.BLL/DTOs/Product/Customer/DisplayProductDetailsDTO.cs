using myshop.BLL.DTOs.ReviewDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Product.Customer
{
    public class DisplayProductDetailsDTO
    {
        public int avgRating {  get; set; }
        public int reviewCount { get; set; }
        public List<DisplayReviewDTO> displayReviewDTOs { get; set; } 
    }
}
