﻿using AutoMapper;
using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Mappings
{
    public class TypeOfSaleProfile:Profile
    {
        public TypeOfSaleProfile()
        {
            CreateMap<TypeOfSale, SaveTypeOfSaleDto>()
               .ReverseMap()
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfSale, TypeOfSaleDto>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());
        }
    }
}
