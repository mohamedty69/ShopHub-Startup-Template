using System;
using System.Collections.Generic;
using System.Security.Cryptography.Pkcs;
using System.Text;

namespace myshop.BLL.DTOs.ReviewDTO
{
    public class EditReviewDTO
    {
        public int reviewId {  get; set; }
        public string comment {  get; set; }
        public int rating { get; set; }
    }
}
