using AutoMapper;
using myshop.BLL.DTOs.Cart;
using myshop.BLL.DTOs.Product.Customer;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.ProductMapping.IncomingData.Customer
{
    public class AddProductToCartProfile : Profile
    {
        public AddProductToCartProfile()
        {
            CreateMap < Product, CartItem>()
                .ForMember(destinationMember: dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destinationMember: dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(destinationMember: dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Img.Replace('\\', '/')))
                .ForMember(destinationMember: dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
