using myshop.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Order
{
    public class SummaryOrderDTO
    {
        public int Id { get; set; } 
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}
