using AutoMapper;
using myshop.BLL.DTOs.Order;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.OrderMapping.OutgoingData
{
    public class SummaryOrderProfile : Profile
    {
        public SummaryOrderProfile()
        {
            CreateMap<OrderHeader, SummaryOrderDTO>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderDetails));
        }
    }
}
