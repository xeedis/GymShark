﻿using AutoMapper;
using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ItemDto>()
                .ForMember(dest=>dest.PhotoUrl, 
                opt=> opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.isMain).Url));
            CreateMap<Photo, PhotoDto>();
            CreateMap<ItemUpdateDto, Product>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Opinion, OpinionDto>();
        }
    }
}
