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
        public int Quantity { get; set; }
    }
}
