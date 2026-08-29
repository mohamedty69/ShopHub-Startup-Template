using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Order
{
    public class OrderItemDTO
    {
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
