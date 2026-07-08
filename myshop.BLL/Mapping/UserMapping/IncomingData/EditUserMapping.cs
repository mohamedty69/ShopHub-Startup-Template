using AutoMapper;
using myshop.BLL.DTOs.User;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace myshop.BLL.Mapping.UserMapping.IncomingData
{
    public class EditUserMapping : Profile
    {
        public EditUserMapping()
        {

            CreateMap<EditUserDTO, ApplicationUser>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => src.IsLocked));
        }
    }
}
