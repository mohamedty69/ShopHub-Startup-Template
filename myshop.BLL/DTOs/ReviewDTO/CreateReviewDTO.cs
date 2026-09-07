using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.ReviewDTO
{
    public class CreateReviewDTO
    {
        public string userId {  get; set; }
        public int productId { get; set; }
        public string comment { get; set; }
        public int rating { get; set; }
    }
}
