using myshop.BLL.DTOs.ReviewDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Product.Customer
{
    public class DisplayProductDetailsDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public double avgRating { get; set; } = 0.0;
        public int reviewCount { get; set; } = 0;
        public IEnumerable<DisplayReviewDTO> Reviews { get; set; } = Enumerable.Empty<DisplayReviewDTO>(); 
    }
}
