using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Product
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
    }
}
