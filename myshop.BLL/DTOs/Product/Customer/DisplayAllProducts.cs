using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Product.Customer
{
    public class DisplayAllProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Image {  get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
