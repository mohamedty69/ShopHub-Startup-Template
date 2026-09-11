using AutoMapper;
using myshop.BLL.DTOs.Product.Customer;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.ProductMapping.OutgoingData.Customer
{
    public class ProductDetailsMapping : Profile
    {
        public ProductDetailsMapping()
        {
            CreateMap<Product, DisplayProductDetailsDTO>()
                .ForMember(destinationMember: dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destinationMember: dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(destinationMember: dest => dest.Image, opt => opt.MapFrom(src => src.Img.Replace('\\', '/')))
                .ForMember(destinationMember: dest => dest.Price, opt => opt.MapFrom(src => src.Price));

        }
    }
}
