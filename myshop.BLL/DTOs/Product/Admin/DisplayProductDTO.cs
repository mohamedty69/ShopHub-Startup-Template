using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Product.Admin
{
    public class DisplayProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0.00M;
        public string CategoryName { get; set; }
    }
}
