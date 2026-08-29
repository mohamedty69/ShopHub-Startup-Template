using AutoMapper;
using myshop.BLL.DTOs.Order;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.OrderMapping.IncomingData
{
    public class CreateOrderProfile : Profile
    {
        public CreateOrderProfile()
        {
            CreateMap<CheckOutOrderDTO, OrderHeader>();
        }
    }
}
