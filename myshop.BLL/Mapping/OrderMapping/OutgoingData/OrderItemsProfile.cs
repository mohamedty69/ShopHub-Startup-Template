using AutoMapper;
using myshop.BLL.DTOs.Order;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.OrderMapping.OutgoingData
{
    public class OrderItemsProfile : Profile
    {
        public OrderItemsProfile()
        {
            CreateMap<OrderDetail,OrderItemDTO>();
        }
    }
}
