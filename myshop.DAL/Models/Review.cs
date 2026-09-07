using Microsoft.Identity.Client;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace myshop.DAL.Models
{
    public class Review : ISoftDelete, IAuditable
    {
        public int reviewId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string userId { get; set; }
        public  Product Product { get; set; }
        public int productId { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
    }
}
