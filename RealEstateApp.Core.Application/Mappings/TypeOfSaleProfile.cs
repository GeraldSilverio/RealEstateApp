using AutoMapper;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Application.Features.TypeOfSales.Command.CreateTypeOfSale;
using RealEstateApp.Core.Application.Features.TypeOfSales.Command.UpdateTypeOfSale;
using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Application.ViewModel.TypeOfSale;
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
            
            CreateMap<TypeOfSale, CreateTypeOfSaleCommand>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());  
            
            CreateMap<TypeOfSale, UpdateTypeOfSaleCommand>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore()); 
            
            
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfSale, TypeOfSaleViewModel>()
                .ReverseMap()
                .ForMember(x => x.RealEstates, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfSale, SaveTypeOfSaleViewModel>()
                .ReverseMap()
                .ForMember(x => x.RealEstates, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfSaleDto, TypeOfSaleViewModel>()
                .ReverseMap();

            CreateMap<TypeOfSaleDto, SaveTypeOfSaleViewModel>()
                .ReverseMap();
        }
    }
}
