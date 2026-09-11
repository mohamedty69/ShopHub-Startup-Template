using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace myshop.BLL.DTOs.ReviewDTO
{
    public class DisplayReviewDTO
    {
        public string comment {  get; set; }
        public int rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
