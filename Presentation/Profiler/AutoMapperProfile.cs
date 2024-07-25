﻿using Domain.Entities;
using Shared.DTOs.AppDTOs;
using AutoMapper;
using System.Text.Json;

namespace Presentation.Profiler
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddAppDTO, App>()
                .ForMember(dest => dest.SEO, opt => opt.MapFrom(src => src.SEO.ToString()))
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.Style.ToString()));
        }
    }
}
