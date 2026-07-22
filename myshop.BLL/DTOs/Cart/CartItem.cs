using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; } 
        public string ProductName { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
